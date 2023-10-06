using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerData _data;
        private PlayerAgent _agent;

        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private AudioClip _pickUpClip;

        [SerializeField] private Transform _groundGizmo;

        [SerializeField] private bool _isGrounded;


        private void Start()
        {
            _data = GetComponent<PlayerData>();
            _agent = GetComponent<PlayerAgent>();

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

            //if (moveX < 0f)
            //    _spriteRenderer.flipX = true;

            //else
            //    _spriteRenderer.flipX = false;


            if (_isGrounded && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                _rigidbody.AddForce(Vector2.up * _data.JumpSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
                _isGrounded = false;
                _agent.Jump();

            }

            //MOVEMENT
            //gravity info should be added at the end (0f)
            Vector2 newVelocity = new Vector2(moveX * _data.MoveSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);
            _rigidbody.velocity = newVelocity;

            _agent.Move(_rigidbody.velocity);

            
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Coin")
            {
                _data.isCollected += collision.GetComponent<PickUp>().GetPickUp();
                AudioManager.Instance.PlaySound(_pickUpClip);
            }
        }
    }

}