using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame
{
    public class PlayerController : CustomBehaviur
    {
        private PlayerData _data;
        public PlayerData Data { get { return _data; } }
        private PlayerAgent _agent;

        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private AudioClip _pickUpClip;

        [SerializeField] private Transform _groundGizmo;

        [SerializeField] private bool _isGrounded;

        [SerializeField] private bool _isPaused;


        private void Start()
        {
            _data = GetComponent<PlayerData>();
            _agent = GetComponent<PlayerAgent>();

            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();

            if (_groundGizmo == null)
                _groundGizmo = transform.Find("Gizmo");
            // _groundGizmo = GetComponentInChildren<Transform>();

            _isPaused = false;

        }

        public override void Init(GameManager gameManager)
        {
            base.Init(gameManager);
            _gameManager = gameManager;
            _gameManager.OnLevelStarted += StartNewLevel;
            _gameManager.OnLevelComplated+= LevelFinished;
        }
        private void OnDestroy()
        {
            _gameManager.OnLevelStarted -= StartNewLevel;
            _gameManager.OnLevelComplated -= LevelFinished;
        }

        private void StartNewLevel()
        {
            _data.isCollected = 0;
            transform.position = Vector3.zero;
            _agent.StopAnimations();
            _isPaused = false;
        }
        private void LevelFinished()
        {
            _isPaused = true;
        }

        void FixedUpdate()
        {
            if (_isPaused)
                return;

            //JUMP
            int layerMask = LayerMask.GetMask("Floor");

            _isGrounded = Physics2D.OverlapPoint(_groundGizmo.position, layerMask);

            float moveX = Input.GetAxis("Horizontal");


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
                _gameManager.audioPlayer.PlaySound(_pickUpClip);
            }

            else if(collision.tag == "Finish")
            {
                _gameManager.CheckIfLevelEnded();
            }
        }
    }

}