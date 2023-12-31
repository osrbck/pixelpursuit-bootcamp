using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform _target;

    private  Vector3 _offsetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _offsetPosition = _target.position -  transform.position;
    }

    private void LateUpdate() {
        transform.position = _target.position - _offsetPosition;

        transform.LookAt(_target.position);
    }
}
