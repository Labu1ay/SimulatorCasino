using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {

    private Icon _tempIcon;
    
    void Update() {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Icon icon = hit.collider.GetComponent<Icon>();
            if(icon) {
                _tempIcon = icon;
                _tempIcon.IncreaseScale();
            }
        } else {
            _tempIcon.DecreaseScale();
        }
    }
}