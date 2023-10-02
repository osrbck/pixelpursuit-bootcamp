using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

[SerializeField]
private Rigidbody _rb;

[SerializeField]
private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

private void FixedUpdate()
{
    _rb.velocity = transform.forward * _speed * Time.fixedDeltaTime;
    _rb.angularVelocity = Vector3.zero;
}

private void OnCollisionEnter(Collision col)
{
    
    Destroy(gameObject, 0.1f);
}

}
