using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CylinderState {
    Idle,
    Rotate
}
public class RotateCylinder : MonoBehaviour {

    private Rigidbody _rigidbody;
    public float SpeedRotation;
    public float Damper;
   // public Transform FrontPosition;
   // public Transform [] CylinderPosition = new Transform [12];
    [SerializeField] private CylinderState _currentCylinderState;
    [SerializeField] private Vector3[] _CylinderPosition = new Vector3[12];
    void Start() {
        _currentCylinderState = CylinderState.Idle;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = Mathf.Infinity;
    }

    private void Update() {

        if (_currentCylinderState == CylinderState.Idle) {
            StopCylinder();
          //  transform.rotation = Quaternion.Lerp(transform.rotation, GetClosest().rotation, Time.deltaTime);
        } else if (_currentCylinderState == CylinderState.Rotate) {

            if (_rigidbody.angularVelocity.x > -Damper) {
               
                SetState(CylinderState.Idle);
                
            }
        }
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SetState(CylinderState.Rotate);
            _rigidbody.AddRelativeTorque(-SpeedRotation, 0f, 0f);
        }
    }

    void SetState(CylinderState state) {
        _currentCylinderState = state;
    }

    void StopCylinder() {
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }

    // Transform GetClosest() {
    //    float minDistance = Mathf.Infinity;
    //    Transform closestVerge = null;
    //    for (int i = 0; i < CylinderPosition.Length; i++) {
    //        float distance = Vector3.Distance(FrontPosition.position, CylinderPosition[i].position);
    //        if (distance < minDistance) {
    //            minDistance = distance;
    //            closestVerge = CylinderPosition[i];
    //        }
    //    }
    //    return closestVerge;
    //}
}
