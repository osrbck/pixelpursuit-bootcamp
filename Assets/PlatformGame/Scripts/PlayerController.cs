using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private Animator _animator;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _pickUpClip;

        [SerializeField] private Transform _groundGizmo;

        [SerializeField] private float _moveSpeed;

        [SerializeField] private float _jumpForce;

        [SerializeField] private bool _isGrounded;

        [SerializeField] private int _collected;


        private void Start()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();

            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_groundGizmo == null)
                _groundGizmo = transform.Find("Gizmo");
            // _groundGizmo = GetComponentInChildren<Transform>();

            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            _collected = 0;


        }

        void FixedUpdate()
        {
            //JUMP
            int layerMask = LayerMask.GetMask("Floor");

            _isGrounded = Physics2D.OverlapPoint(_groundGizmo.position, layerMask);

            float moveX = Input.GetAxis("Horizontal");

            if (moveX < 0f)
                _spriteRenderer.flipX = true;

            else
                _spriteRenderer.flipX = false;


            if (_isGrounded && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);

                _animator.SetTrigger("Jump");
            }

            //MOVEMENT
            //gravity info should be added at the end (0f)
            Vector2 newVelocity = new Vector2(moveX * _moveSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);

            _rigidbody.velocity = newVelocity;

            if (_rigidbody.velocity.y < 0f)
                _animator.SetBool("isFalling", true);

            else
                _animator.SetBool("isFalling", false);

            if (_rigidbody.velocity.x != 0f)
                _animator.SetBool("isWalking", true);

            else
                _animator.SetBool("isWalking", false);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Coin")
            {
                _collected += collision.GetComponent<PickUp>().GetPickUp();
                _audioSource.PlayOneShot(_pickUpClip);
            }
        }
    }

}