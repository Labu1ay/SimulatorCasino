using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Button Button;
    public RotateCylinder RotateCylinder;

    private RotateCylinder[] _rotateCylinders;

    public Outline Outline;
    private Color _color;
    void Start() {
        _color = Outline.effectColor;
        _rotateCylinders = FindObjectsOfType<RotateCylinder>();
    }

    void Update() {
        if (RotateCylinder.CurrentCylinderState == CylinderState.Rotate) {
            Button.interactable = false;
            Outline.effectColor = Color.red;
        } else {
            Button.interactable = true;
            Outline.effectColor = _color;
        }
    }

    public void PressButton() {
        for (int i = 0; i < _rotateCylinders.Length; i++) {
            _rotateCylinders[i].StartRotationForButton();
        }
    }
}
