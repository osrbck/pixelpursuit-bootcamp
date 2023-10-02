using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private Transform _groundGizmo;

        [SerializeField] private float _moveSpeed;

        [SerializeField] private float _jumpForce;

        [SerializeField] private bool _isGrounded;


        private void Start()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();

            if (_groundGizmo == null)
                _groundGizmo = transform.Find("Gizmo");
            // _groundGizmo = GetComponentInChildren<Transform>();


        }

        void FixedUpdate()
        {
            //JUMP
            int layerMask = LayerMask.GetMask("Floor");

            _isGrounded = Physics2D.OverlapPoint(_groundGizmo.position, layerMask);

            float moveX = Input.GetAxis("Horizontal");

            if (_isGrounded && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }

            //MOVEMENT
            //gravity info should be added at the end (0f)
            Vector2 newVelocity = new Vector2(moveX * _moveSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);

            _rigidbody.velocity = newVelocity;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // if (collision.collider.tag == "Platform")
            //     _isGrounded = true;
        }
    }

}