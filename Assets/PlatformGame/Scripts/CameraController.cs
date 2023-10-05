using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame { 

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 0.125f;


        private void LateUpdate()
        {
            if (_target == null)
                return;

            Vector3 desiredPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

}