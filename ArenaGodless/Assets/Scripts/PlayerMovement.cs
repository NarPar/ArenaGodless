using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    public Vector2 Direction { get { return _dir; } }
    public float SpeedMultiplier = 1f;

    private Rigidbody2D _rb;
    private Vector2 _dir = Vector2.down;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(0f, 0f);

        if (Input.GetKey(KeyCode.A)) input.x = -1;
        if (Input.GetKey(KeyCode.D)) input.x = 1;
        if (Input.GetKey(KeyCode.W)) input.y = 1;
        if (Input.GetKey(KeyCode.S)) input.y = -1;

        if (input.x != 0 || input.y != 0)
        {
            _rb.velocity = input * speed * SpeedMultiplier;
            _dir = _rb.velocity.normalized;
        }
    }
}
