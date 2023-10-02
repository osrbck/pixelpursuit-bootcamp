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

    private bool _canAttack;

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

    private void Update()
    {
        _attackTimer += Time.deltaTime;

        if (!_canAttack && _attackTimer > _attackRate)
            Attack();

    }
    private void FixedUpdate()
    {
        if (_canAttack)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;

            return;
        }

        float angleBetween = 270 - Mathf.Atan2(transform.position.z - _target.transform.position.z, transform.position.x - _target.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angleBetween, 0);

        _rb.velocity = transform.forward * _moveSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Bullet")
        {
            _currentHealth -= 1;

            if (_currentHealth <= 0)
            {
                _animator.SetBool("isDead", true);
                _target.EnemyKilled();
                Destroy(gameObject, 1f);
            }
            else
            {
                _animator.SetTrigger("gotHit");
            }

        }

        if (col.collider.tag == "Player")
        {
            _canAttack = true;

        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            _canAttack = false;

        }
    }

    private void Attack()
    {
        _attackTimer = 0f;
        _animator.SetTrigger("Attack");
        _target.GotHit(1);
    }
}


