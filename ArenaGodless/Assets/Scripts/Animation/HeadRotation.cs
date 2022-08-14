using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    [SerializeField] float shoulderHeight = 1.5f;

    private Rigidbody2D _parentRB;
    private SpriteRenderer _renderer;
    private SortByPos _sortByPos;

    private Vector2 _downPosition;
    private int _frontSortingOffset;
    private Vector2 _lastVel = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        _parentRB = GetComponentInParent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _sortByPos = GetComponent<SortByPos>();

        _frontSortingOffset = _sortByPos.Offset;

        _downPosition = new Vector3(0, shoulderHeight, 0) - transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = _parentRB.velocity;
        float angle = Vector2.SignedAngle(_downPosition, vel);

        if (_lastVel != vel)
        {
            Vector2 newPos = _downPosition.Rotate(angle);

            // sort the head behind the body when moving upwards
            int newSort = /*(newPos.y - shoulderHeight)*/ vel.y <= 0 ? _frontSortingOffset : _frontSortingOffset * -1;
            if (newSort != _sortByPos.Offset)
            {
                _sortByPos.Offset = newSort;
            }

            newPos.y += shoulderHeight;
            transform.localPosition = newPos;
        }

        _lastVel = vel;
    }
}
