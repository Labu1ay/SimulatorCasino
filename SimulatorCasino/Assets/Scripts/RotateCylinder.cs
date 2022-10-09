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

    private bool _checkPressButton;

    public ParticleSystem ParticleSystemIcon;

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
                StopCylinder(); 
            }
        }  
    }
    void FixedUpdate() {
        if (_checkPressButton) {
            SetState(CylinderState.Rotate);
            Rigidbody.AddRelativeTorque(-Random.Range(SpeedRotation-50f, SpeedRotation+50f), 0f, 0f); //assign a random torque to the cylinder
            _checkPressButton = false;
        }
    }

    void StopCylinder() {
        Rigidbody.isKinematic = true;
        Rigidbody.isKinematic = false;
        Invoke("StartParticleSystem", 0.2f);
        AudioStopCylinder.PlayOneShot(AudioStopCylinder.clip);
        SetState(CylinderState.Idle);
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

    public void StartRotationForButton() => _checkPressButton = true;
    void SetState(CylinderState state) => CurrentCylinderState = state;
    void StartParticleSystem() => ParticleSystemIcon.Play();
}
