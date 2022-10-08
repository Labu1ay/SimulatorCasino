using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Button [] Buttons;
    public RotateCylinder RotateCylinder;

    private RotateCylinder[] _rotateCylinders;

    void Start() {
        _rotateCylinders = FindObjectsOfType<RotateCylinder>();
    }

    void Update() {
        for (int i = 0; i < Buttons.Length; i++) {

            if (RotateCylinder.CurrentCylinderState == CylinderState.Rotate) {
                Buttons[i].interactable = false;
            } else {
                Buttons[i].interactable = true;
            }
        }
    }

    public void PressButton() {
        for (int i = 0; i < _rotateCylinders.Length; i++) {
            _rotateCylinders[i].StartRotationForButton();
        }
    }
}
