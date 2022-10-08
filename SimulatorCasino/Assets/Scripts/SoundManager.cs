using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource AudioRotationCylinder;
    public RotateCylinder RotateCylinder;
    private int _oneAction;
    void Start()
    {
        
    }

  
    void LateUpdate()
    {
        if(RotateCylinder.CurrentCylinderState == CylinderState.Rotate) {
            if (_oneAction == 0) {
                AudioRotationCylinder.Play();
                _oneAction += 1;
            }
        } else {
            AudioRotationCylinder.Stop();
            _oneAction = 0;
        }
    }
}
