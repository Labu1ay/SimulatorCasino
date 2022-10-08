using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Button [] Buttons;
    public RotateCylinder RotateCylinder;

    private RotateCylinder[] _rotateCylinders;

    public Outline Outline;
    private Color _color;
    void Start() {
        _color = Outline.effectColor;
        _rotateCylinders = FindObjectsOfType<RotateCylinder>();
    }

    void Update() {
        for (int i = 0; i < Buttons.Length; i++) {

            if (RotateCylinder.CurrentCylinderState == CylinderState.Rotate) {
                Buttons[i].interactable = false;
                Outline.effectColor = Color.red;
            } else {
                Buttons[i].interactable = true;
                Outline.effectColor = _color;

            }
        }
    }

    public void PressButton() {
        for (int i = 0; i < _rotateCylinders.Length; i++) {
            _rotateCylinders[i].StartRotationForButton();
        }
    }
}
