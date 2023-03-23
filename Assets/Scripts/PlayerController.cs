using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  InGameDebug debugScreen;
  InGameDebug statusScreen;

  // Start is called before the first frame update
  void Start() {
    debugScreen = GameObject.Find("DebugScreen").GetComponent<InGameDebug>();
    statusScreen = GameObject.Find("StatusScreen").GetComponent<InGameDebug>();
  }

  // Update is called once per frame
  void Update() {
    statusScreen.Blank();
    GameObject rightAnchor = GameObject.Find("/OVRCameraRig/TrackingSpace/RightHandAnchor");
    GameObject leftAnchor = GameObject.Find("/OVRCameraRig/TrackingSpace/LeftHandAnchor");

    statusScreen.Log("RightAnchor pos: "+ rightAnchor.transform.position);
    statusScreen.Log("LeftAnchor  pos: "+ leftAnchor.transform.position);
    statusScreen.Log("RightAnchor Rigidbody pos: "+ rightAnchor.GetComponent<Rigidbody>().position);
    statusScreen.Log("LeftAnchor  Rigidbody pos: "+ leftAnchor.GetComponent<Rigidbody>().position);
    

    if(OVRInput.Get(OVRInput.RawButton.A) || Input.GetKeyDown(KeyCode.X)) {
      Transform controllerTrans = GameObject.Find("/OVRCameraRig/TrackingSpace/RightHandAnchor").transform;
      Transform malletTrans = GameObject.Find("Mallet").transform;

			debugScreen.Log("[PlayerController]: Moving mallet to ("+controllerTrans.position.x+", "+controllerTrans.position.y+", "+controllerTrans.position.z+")");
      
      malletTrans.position = controllerTrans.position;
			malletTrans.rotation = controllerTrans.rotation * Quaternion.Euler(45, 0, 0);
    }

    if(OVRInput.Get(OVRInput.RawButton.B) || Input.GetKeyDown(KeyCode.R)) {
      // Almost works, but something is fucky. Maybe just delete mallet and spawn new one at the hand? All depends on the hand transform.
      Transform malletTrans = GameObject.Find("Mallet").transform;
      Vector3 newPos = new Vector3(0.3f, 10.2f, -19.5f);
      Quaternion newRot = Quaternion.Euler(90, 0, -45);

			debugScreen.Log("[PlayerController]: Moving mallet to ("+newPos.x+", "+newPos.y+", "+newPos.z+")");
      
      malletTrans.position = newPos;
			malletTrans.rotation = newRot;
    }
  }
}
