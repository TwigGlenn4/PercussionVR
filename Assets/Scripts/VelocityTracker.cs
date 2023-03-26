using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTracker : MonoBehaviour {
  private Vector3 previousPosition;
  public float velocity { get; private set; }
  // Start is called before the first frame update
  void Start() {
    previousPosition = transform.position;
  }

  // Update is called once per frame
  void Update() {
    Vector3 deltaV = ( transform.position - previousPosition ) / Time.deltaTime;
    velocity = deltaV.magnitude;
    previousPosition = transform.position;
  }
}
