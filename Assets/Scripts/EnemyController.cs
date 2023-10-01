using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Rigidbody _rb;
    private Animator _animator;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private PlayerController _target;

    [SerializeField] private float _maxHealth;

    private float _currentHealth;


    private float _attackRate;
    private float _attackTimer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _target = GameObject.Find("Player").GetComponent<PlayerController>();

        _currentHealth = _maxHealth;
        _attackTimer = 0f;
    }

    private void FixedUpdate()
    {
        _attackTimer += Time.fixedDeltaTime;

        float angleBetween = 270 - Mathf.Atan2(transform.position.z - _target.transform.position.z, transform.position.x - _target.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angleBetween, 0);

        _rb.velocity = transform.forward * _moveSpeed * Time.fixedDeltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Bullet")
        {
            _currentHealth -= 1;

            if (_currentHealth <= 0)
            {
                _animator.SetBool("isDead", true);
                Destroy(gameObject, 1f);
            }
            else
            {
                _animator.SetTrigger("GotHit");
            }

        }

        if (col.collider.tag == "Player")
        {
            if (_attackTimer > _attackRate)
                Attack();

        }
    }

    private void Attack()
    {
        _attackTimer = 0f;
        _animator.SetTrigger("Attack");
    }
}


