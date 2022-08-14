using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can be hit by player's weapon attacks
public class Attackable : MonoBehaviour
{
    [SerializeField] GameObject attackedPrefab = null;
    [SerializeField] int healthPoints = 100;

    private SpriteRenderer[] _spriteRenderers;
    private Color[] _startingColors;

    private float _flashTimer = 0f;
    private float _flashTime = 0.5f;

    public void Attack(int damage, Vector2 dir)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
        {
            if (attackedPrefab != null)
            {
                GameObject obj = Instantiate(attackedPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null) rb.AddForce(dir * 1000f);
                Transform blood = obj.transform.Find("Blood");
                if (blood != null)
                {
                    Vector3 splashDir = new Vector3(0f, 0f, 0f);
                    if (dir.x > 0f) splashDir.y = 45f;
                    if (dir.x < 0f) splashDir.y = -45f;
                    if (dir.y > 0f) splashDir.x = -45f;
                    if (dir.y < 0f) splashDir.x = 45f;
                    blood.Rotate(splashDir);
                }
            }
            Destroy(gameObject);
        }
        else
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(dir * 1000f);

            _flashTimer = _flashTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _startingColors = new Color[_spriteRenderers.Length];
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _startingColors[i] = _spriteRenderers[i].color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_flashTimer > 0f)
        {
            _flashTimer -= Time.deltaTime;
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                _spriteRenderers[i].color = Color.Lerp(Color.white, _startingColors[i], 1f - (_flashTimer / _flashTime));
            }
        }
    }
}
