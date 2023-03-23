using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class Instrument : MonoBehaviour {
	
 public Material[] instrumentMaterials;
 Renderer rend;
 InGameDebug debugScreen;
 MidiStreamPlayer midiStreamPlayer;

 [Range(0,127)]
 public int notePitch;
 [Range(0,127)]
 public int instChannel;
 public OVRInput.RawButton hotkey = OVRInput.RawButton.None;


  // Start is called before the first frame update
  void Start() {

     // Assigns the component's renderer instance
     rend = GetComponent<Renderer>();
     rend.enabled = true;

     debugScreen = GameObject.Find("DebugScreen").GetComponent<InGameDebug>();
     midiStreamPlayer = FindObjectOfType<MidiStreamPlayer>();
  }
  private void OnCollisionEnter(Collision col) {
    // Checks if the drum is hit by mallet
    GameObject hitBy = col.GetContact(0).otherCollider.gameObject;

    // debugScreen.Log("[Instrument]: Other Contact point is "+ hitBy.name);

    if( hitBy.name == "MalletHead") {
      // debugScreen.Log("[Instrument]: Velocity = "+ col.relativeVelocity);

      OVRPlugin.SystemHeadset headset = OVRPlugin.GetSystemHeadsetType();

      int velocity = 100;

      // Get controller's velocity, or mallet's velocity in Editor Run
      if( headset == OVRPlugin.SystemHeadset.Oculus_Quest_2 ) {        
        GameObject hitAnchor = hitBy.transform.parent.parent.gameObject;
        OVRInput.Controller controllerType = hitAnchor.transform.GetChild(0).GetChild(0).gameObject.GetComponent<OVRRuntimeController>().m_controller;
        float rawVelocity = OVRInput.GetLocalControllerVelocity(controllerType).sqrMagnitude;
        velocity = (int)Mathf.Clamp(Mathf.Log((5*rawVelocity)+1, 2)*17 , 0, 127);
        
        // debugScreen.Log("[Instrument]: "+hitAnchor.name+" velocity = "+hitAnchor.GetComponent<Rigidbody>().velocity);
        // debugScreen.Log("[Instrument]: controllerType = "+controllerType);
        // debugScreen.Log("[Instrument]: velocity = "+velocity+", sqrVelocity = "+OVRInput.GetLocalControllerVelocity(controllerType).sqrMagnitude);
        debugScreen.Log("[Instrument]: "+controllerType+" hit "+gameObject.name+" at velocity "+velocity );
      }
      else {
        GameObject mallet = hitBy.transform.parent.gameObject;
        debugScreen.Log("[Instrument]: "+mallet.name+" velocity = "+mallet.GetComponent<Rigidbody>().velocity);
      }

      rend.sharedMaterial = instrumentMaterials[1];
      MPTKEvent noteEvent = PlayNote(velocity);
      
    }
  }
	private void OnCollisionExit(Collision col) {
    if (col.gameObject.name == "Mallet") {
      rend.sharedMaterial = instrumentMaterials[0];
    }
 }
  // Update is called once per frame
  void Update() {
      if(OVRInput.Get(hotkey) ) {
        PlayNote(100);
      }
  }
  private MPTKEvent PlayNote(int velocity) {
    MPTKEvent noteEvent = new MPTKEvent() {
      Command = MPTKCommand.NoteOn,
      Value = notePitch,
      Channel = instChannel,
      Duration = 500,
      Velocity = velocity,
      Delay = 0
    };
    midiStreamPlayer.MPTK_PlayEvent(noteEvent);
    return noteEvent;
  }
}
