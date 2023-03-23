using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameDebug : MonoBehaviour {

  TMP_Text debugText;

  // Start is called before the first frame update
  void Start() {
    debugText = gameObject.transform.Find("Canvas/DebugText").gameObject.GetComponent<TextMeshProUGUI>();
		debugText.text = "Debug text found!";
  }
	public void Log(string msg) {
		debugText.text += msg+"\n";

		while( debugText.preferredHeight >= 800 ) {
			int index = debugText.text.IndexOf('\n');
			debugText.text = debugText.text.Substring(index + 1);
			Debug.Log("Removed first line.");
		}
	}
	public void Blank() {
		debugText.text = "";
	}
}
