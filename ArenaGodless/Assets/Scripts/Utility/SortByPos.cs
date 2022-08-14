using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortByPos : MonoBehaviour
{
    [SerializeField] Transform target = null;

    public void SetTarget(Transform t)
    {
        target = t;
    }
    
    public int Offset = 0;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sortingOrder = -100 * (int)target.position.y + Offset;
    }
}
