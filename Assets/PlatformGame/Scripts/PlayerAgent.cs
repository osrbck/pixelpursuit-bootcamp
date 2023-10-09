using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlatformGame
{
    public class PlayerAgent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Jump()
        {
            _animator.SetTrigger("Jump");

        }

        public void Move(Vector2 velocity)
        {
            if (velocity.x < 0f)
                _spriteRenderer.flipX = true;

            else
                _spriteRenderer.flipX = false;

            if (velocity.y < 0f)
                _animator.SetBool("isFalling", true);

            else
                _animator.SetBool("isFalling", false);

            if (velocity.x != 0f)
                _animator.SetBool("isWalking", true);

            else
                _animator.SetBool("isWalking", false);
        }

        public void StopAnimations()
        {
            _animator.SetBool("isFalling", false);
            _animator.SetBool("isWalking", false);
            _animator.playbackTime = 0f;
        }
    }
}