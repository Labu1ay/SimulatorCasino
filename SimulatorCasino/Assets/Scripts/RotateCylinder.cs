using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CylinderState {
    Idle,
    Rotate
}
public class RotateCylinder : MonoBehaviour {

    public Rigidbody Rigidbody;
    public float SpeedRotation;
    public float Damper;
    private Quaternion _closestVerge;
    [SerializeField] private CylinderState _currentCylinderState;

    [SerializeField] private Vector3[] _cylinderRotationEuler = new Vector3[12];
    private Quaternion[] _cylinderRotation = new Quaternion[12];

    void Start() {

        _currentCylinderState = CylinderState.Idle;
        Rigidbody.maxAngularVelocity = Mathf.Infinity;

        for (int i = 0; i < _cylinderRotationEuler.Length; i++) {
            _cylinderRotation[i] = Quaternion.Euler(_cylinderRotationEuler[i]);
        }

    }

    private void Update() {

        if (_currentCylinderState == CylinderState.Idle) {
            transform.rotation = Quaternion.Lerp(transform.rotation, GetClosest(), Time.deltaTime * 3f);
        } else if (_currentCylinderState == CylinderState.Rotate) {

            if (Rigidbody.angularVelocity.x > -Damper) {
                StopCylinder();
                SetState(CylinderState.Idle);
            }
        }  
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SetState(CylinderState.Rotate);
            Rigidbody.AddRelativeTorque(-Random.Range(SpeedRotation-150f, SpeedRotation+150f), 0f, 0f);
        }
    }

    void SetState(CylinderState state) => _currentCylinderState = state;
    

    void StopCylinder() {
        Rigidbody.isKinematic = true;
        Rigidbody.isKinematic = false;
    }

    Quaternion GetClosest() {
        float minAngle = Mathf.Infinity;

        for (int i = 0; i < _cylinderRotation.Length; i++) {
            float angle = Quaternion.Angle(transform.rotation, _cylinderRotation[i]);
            if (angle <= minAngle) {
                minAngle = angle;
                _closestVerge = _cylinderRotation[i];
            }
        }
        return _closestVerge;
    }
}
