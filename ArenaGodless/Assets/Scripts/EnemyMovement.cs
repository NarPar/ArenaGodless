using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float attackRange = 2f;

    private Rigidbody2D _rb;

    private Transform _target;
    private EnemyAttackable _targetAttackable;
    private Vector2 _lastDir = new Vector2(0f,0f);

    private bool _attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            LayerMask mask = LayerMask.GetMask("Player");
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 2f, _lastDir, attackRange * 3f, mask);

            Debug.DrawLine(transform.position, transform.position + (Vector3)(_lastDir * attackRange * 3f), Color.red);

            if (hit.transform != null)
            {
                Debug.Log("Enemy: Target Acquired = " + hit.transform.name);
                _target = hit.transform;
                _targetAttackable = hit.transform.GetComponent<EnemyAttackable>();
            }
        }

        if (_target != null)
        {
            MoveToTarget();
        }

        _lastDir = _rb.velocity.normalized;
    }

    private void MoveToTarget()
    {
        Vector2 dir = _target.position - transform.position;

        if (dir.magnitude > attackRange)
        {
            _rb.velocity = dir.normalized * speed;

            if (_attacking)
            {
                if (_targetAttackable != null) _targetAttackable.RemoveAttacker(this);
                _attacking = false;
            }
        }
        else
        {
            if (!_attacking)
            {
                if (_targetAttackable != null) _targetAttackable.AddAttacker(this);
                _attacking = true;
            }
        }
    }

    private void OnDestroy()
    {
        if (_attacking)
        {
            if (_targetAttackable != null) _targetAttackable.RemoveAttacker(this);
        }
    }
}
