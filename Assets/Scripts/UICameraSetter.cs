using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Set this script on the UI object, parent of the Canvas
public class UICameraSetter : MonoBehaviour {
  void Awake() {
    transform.Find("Canvas").gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("CanvasPointer").GetComponent<Camera>();
    
  }
}
