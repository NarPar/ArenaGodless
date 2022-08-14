using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] float pickupRange = 0.5f;

    public Weapon Weapon { get { return _weapon; } }

    private Weapon _weapon = null;

    private PlayerMovement _movement;

    public void Pickup(Weapon weapon)
    {
        if (_weapon != null)
        {
            _weapon.transform.parent = null;
            _weapon.GetComponentInChildren<SortByPos>().SetTarget(_weapon.transform);
        }

        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.GetComponentInChildren<SortByPos>().SetTarget(_movement.transform);

        _weapon = weapon;
    }

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_weapon == null) TryPickup();
            else
            {
                _weapon.Throw(_movement.gameObject, _movement.Direction);
                _weapon = null;
            }
        }

        if ((_movement.Direction.y < 0 || _movement.Direction.x < 0) && transform.localPosition.x > 0 
            || (_movement.Direction.y > 0 || _movement.Direction.x > 0) && transform.localPosition.x < 0)
        {
            Vector3 p = transform.localPosition;
            p.x *= -1f;
            transform.localPosition = p;

            if (_weapon)
            {
                SpriteRenderer sr = _weapon.GetComponentInChildren<SpriteRenderer>();
                sr.flipX = transform.localPosition.x < 0;
            }
        }
    }

    private void TryPickup()
    {
        LayerMask mask = LayerMask.GetMask("Item");
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 2f, _movement.Direction, pickupRange * 3f, mask);

        Debug.DrawLine(transform.position, transform.position + (Vector3)(_movement.Direction * pickupRange * 3f), Color.red);

        if (hit.transform != null)
        {
            Debug.Log("Arm: Item Picked Up = " + hit.transform.name);
            Weapon weapon = hit.transform.GetComponent<Weapon>();
            if (weapon != _weapon) Pickup(weapon);
        }
    }
}
