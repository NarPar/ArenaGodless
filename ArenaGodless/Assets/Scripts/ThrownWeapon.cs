using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
    [SerializeField] GameObject normalWeapon = null;
    [SerializeField] int damage = 1;
    [SerializeField] float range = 5f;

    private Rigidbody2D _rigidbody2D;
    private float _distance = 0f;
    private Vector2 _lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = (Vector2)transform.position - _lastPosition;
        _distance += Mathf.Abs(distance.magnitude);
        _lastPosition = (Vector2)transform.position;

        if (_distance >= range)
        {
            StopBeingThrown();
        }
    }

    private void StopBeingThrown()
    {
        Instantiate(normalWeapon, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attackable attackable = collision.GetComponent<Attackable>();
        if (attackable)
        {
            attackable.Attack(damage, _rigidbody2D.velocity.normalized);
            StopBeingThrown();
        }
    }
}
