using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame
{ 

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _jumpForce;

    [SerializeField] private int _collected;

        public float MoveSpeed
        {
            get { return _moveSpeed; }
            set { _moveSpeed = value; }
        }
        public float JumpSpeed
        {
            get { return _jumpForce; }
            set { _jumpForce = value; }
        }
        public int isCollected
        {
            get { return _collected; }
            set { _collected = value; }
        }
        //endFields

        private void Start()
        {
            _collected = 0;
        }
    }
}