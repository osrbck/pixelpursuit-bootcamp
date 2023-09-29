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

   private void Start() {
    _rb = GetComponent<Rigidbody>();
   }

   private void Update() {
    // Vector3 mousePos = _gameCam.ScreenToWorldPoint(Input.mousePosition);
    // Ray mouseRay = _gameCam.ScreenPointToRay(Input.mousePosition);
    // RaycastHit hitInfo;
    // Physics.Raycast(mouseRay, out hitInfo, 100f);

    // if(hitInfo.collider!= null){
    //     transform.LookAt(hitInfo.point);
    // }

   }

   private void FixedUpdate() {
    
    float inputX = Input.GetAxis("Horizontal");
    float inputZ = Input.GetAxis("Vertical");

    Vector3 playerVelocity = new Vector3(inputX, 0, inputZ) * _moveSpeed * Time.fixedDeltaTime;
    playerVelocity.y = _rb.velocity.y;

    _rb.velocity = playerVelocity;

   }

}
