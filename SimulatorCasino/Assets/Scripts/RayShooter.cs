using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {

    private Icon _tempIcon;
    private float _timer;
    private void Update() {
        if (_timer > 0f) {
            return;
        }
        _timer += _timer + Time.deltaTime;
    }
    void FixedUpdate() {
        if (_timer == 0f) {
            return; //it is necessary that the Ray starts working with a delay of one frame
        }

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Icon icon = hit.collider.GetComponent<Icon>();
            if (icon) {
                _tempIcon = icon;

                _tempIcon.IncreaseScale();
            } else {
                Unselect(_tempIcon);
            }
        } else {
            Unselect(_tempIcon);
        }
    }

    void Unselect(Icon icon) {
        if (icon) {
            icon.DecreaseScale();
        }
    }
}