using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    private Vector3 _startScale;
    private Vector3 _selectScale;

    private bool _increase;
    private bool _decrease;

    public Animator Animator;

    void Start()
    {
        _startScale = transform.localScale;
        _selectScale = _startScale * 1.4f;
    }

    public void IncreaseScale() {
        _increase = true;
        _decrease = false;
    }
    public void DecreaseScale() {
        _decrease = true;
        _increase = false;
    }


    void Update()
    {
        if (_decrease) {
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale, Time.deltaTime * 10f);
            TurnOffAnimation();  
        }
        if (_increase) {
            transform.localScale = Vector3.Lerp(transform.localScale, _selectScale, Time.deltaTime * 10f);
            TurnOnAnimation();
        }
    }


    [ContextMenu("TurnOnAnimation")]
    public void TurnOnAnimation() => Animator.SetBool("IconBool", true);

    [ContextMenu("TurnOffAnimation")]
    public void TurnOffAnimation() => Animator.SetBool("IconBool", false);
    
}
