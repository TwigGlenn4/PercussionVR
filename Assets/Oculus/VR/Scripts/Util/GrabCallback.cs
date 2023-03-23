using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCallback : MonoBehaviour {
    
    Rigidbody m_rigidbody;
    // Start is called before the first frame update
    void Start() {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
    }
    public void OnGrab() {
        // m_rigidbody.constraints = RigidbodyConstraints.None;
    }
    public void OnRelease() {
        // m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
