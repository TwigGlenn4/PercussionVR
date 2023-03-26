using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandlerDrum : MonoBehaviour {
  private Transform canvasTransfrom;

  private Button setHotkey;
  private Button velocityScale;
  private Button lockPos;
  private Button deleteInstrument;
  void Awake() {
    canvasTransfrom = transform.Find("Canvas");   

    lockPos = canvasTransfrom.Find("LockButton").gameObject.GetComponent<Button>();
    lockPos.clicked += LockButtonCallback;
  }

  void LockButtonCallback() {
    GameObject instrument = transform.parent.gameObject;
    Instrument instrumentScript = instrument.GetComponent<Instrument>();
    instrumentScript.ToggleLocked();
  }
}
