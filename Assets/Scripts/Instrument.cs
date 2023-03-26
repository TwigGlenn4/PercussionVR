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

  private bool locked = false;


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

    if( hitBy.name == "MalletHead" ) {
      // debugScreen.Log("[Instrument]: Velocity = "+ col.relativeVelocity);
      // Debug.Log("Hit by MalletHead");


      float rawVelocity = hitBy.GetComponent<VelocityTracker>().velocity;
      int velocity = (int)Mathf.Clamp(Mathf.Log((5*rawVelocity)+1, 2)*17 , 0, 127);
      debugScreen.Log("[Instrument]: "+gameObject.name+" hit at velocity "+velocity );

      rend.sharedMaterial = instrumentMaterials[1];
      MPTKEvent noteEvent = PlayNote(velocity);
      
    }
  }
  
	private void OnCollisionExit(Collision col) {
    GameObject hitBy = col.GetContact(0).otherCollider.gameObject;

    if ( hitBy.name == "Mallet" ) {
      rend.sharedMaterial = instrumentMaterials[0];
    }
  }

  public void ToggleLocked() {
    OVRGrabbable grabbable = GetComponent<OVRGrabbable>();

    locked = !locked;
    if( locked ) { // Disable the grabbable if the instrument is now locked
      grabbable.enabled = false;
      debugScreen.Log("[Instrument]: "+gameObject.name+" is Unlocked");
    } 
    else { // Enable the grabbable if the instrument is now unlocked
      grabbable.enabled = true;
      debugScreen.Log("[Instrument]: "+gameObject.name+" is Locked");
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
