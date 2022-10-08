using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CylinderState {
    Idle,
    Rotate
}
public class RotateCylinder : MonoBehaviour {

    public CylinderState CurrentCylinderState;

    public Rigidbody Rigidbody;

    public float SpeedRotation;
    public float Damper;

    private Quaternion _closestVerge;

    [SerializeField] private Vector3[] _cylinderRotationEuler = new Vector3[12]; //stores the correct rotations of the cylinder
    private Quaternion[] _cylinderRotation = new Quaternion[12];

    public AudioSource AudioStopCylinder;

    private void Awake() {
        for (int i = 0; i < _cylinderRotationEuler.Length; i++) {
            _cylinderRotation[i] = Quaternion.Euler(_cylinderRotationEuler[i]); //conversion from euler angles to quaternions
        }
        transform.rotation = _cylinderRotation[Random.Range(0, 12)]; //random position of the cylinder
    }
    void Start() {

        CurrentCylinderState = CylinderState.Idle;
        Rigidbody.maxAngularVelocity = Mathf.Infinity;
    }

    private void Update() {

        if (CurrentCylinderState == CylinderState.Idle) {
            transform.rotation = Quaternion.Lerp(transform.rotation, GetClosest(), Time.deltaTime * 3f);
        } else if (CurrentCylinderState == CylinderState.Rotate) {

            if (Rigidbody.angularVelocity.x > -Damper) {
                AudioStopCylinder.PlayOneShot(AudioStopCylinder.clip);
                StopCylinder();
                SetState(CylinderState.Idle);
            }
        }  
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SetState(CylinderState.Rotate);
            Rigidbody.AddRelativeTorque(-Random.Range(SpeedRotation-100f, SpeedRotation+100f), 0f, 0f); //assign a random torque to the cylinder
        }
    }

    void SetState(CylinderState state) => CurrentCylinderState = state;
    

    void StopCylinder() {
        Rigidbody.isKinematic = true;
        Rigidbody.isKinematic = false;
    }


    //When the cylinder stop - find the closest edge to the screen in order to tighten it
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
