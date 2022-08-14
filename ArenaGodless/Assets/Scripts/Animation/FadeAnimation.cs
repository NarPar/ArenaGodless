using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] float time = 0.25f;

    private SpriteRenderer _spriteRenderer = null;

    private float _timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        Color c = _spriteRenderer.color;
        c.a = 1f - _timer / time;
        _spriteRenderer.color = c;

        if (_timer >= time)
        {
            Destroy(gameObject);
        }
    }
}
