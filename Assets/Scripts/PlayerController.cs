using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private Rigidbody _rb;
   
   [SerializeField] 
   private float _moveSpeed;

   [SerializeField] 
   private Camera _gameCam;

   [SerializeField]
   private GameObject _bulletPrefab;

   [SerializeField]
   private Transform _muzzlePoint;

   [SerializeField]
   private float _rateOffFire;
   private float _fireTimer;


   private void Start() {
    _rb = GetComponent<Rigidbody>();
    _fireTimer = _rateOffFire;
   }

   private void Update() {
    // Vector3 mousePos = _gameCam.ScreenToWorldPoint(Input.mousePosition);
    Ray mouseRay = _gameCam.ScreenPointToRay(Input.mousePosition);
    RaycastHit hitInfo;

    if(Physics.Raycast(mouseRay, out hitInfo, 100f, 1 << 3))
    {
        float angleBetween = 270 - Mathf.Atan2(transform.position.z - hitInfo.point.z, transform.position.x - hitInfo.point.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angleBetween, 0);
    }

    _fireTimer += Time.deltaTime;

    if(Input.GetMouseButton(0))
    {
      if(_fireTimer > _rateOffFire)
         Shoot();
    }
      

   }

   private void Shoot(){
      GameObject go = Instantiate(_bulletPrefab, _muzzlePoint.position, _muzzlePoint.rotation);
      _fireTimer = 0f;
   }

   private void FixedUpdate() {
    
    float inputX = Input.GetAxis("Horizontal");
    float inputZ = Input.GetAxis("Vertical");

    Vector3 playerVelocity = new Vector3(inputX, 0, inputZ) * _moveSpeed * Time.fixedDeltaTime;
    playerVelocity.y = _rb.velocity.y;

    _rb.velocity = playerVelocity;

   }



}
