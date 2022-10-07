using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    private Vector3 _startScale;
    private Vector3 _selectScale;
    void Start()
    {
        _startScale = transform.localScale;
        _selectScale = _startScale * 1.2f;
    }

    public void IncreaseScale() {
        transform.localScale = Vector3.Lerp(_startScale, _selectScale, Time.deltaTime * 3f);
    }

    public void DecreaseScale() {
        transform.localScale = Vector3.Lerp(_selectScale, _startScale, Time.deltaTime * 3f);
    }


    void Update()
    {
        
    }
}
