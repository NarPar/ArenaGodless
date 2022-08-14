using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can be attacked by NPC enemies. Typically a component of player characters.
public class EnemyAttackable : MonoBehaviour
{
    [SerializeField] float endurance = 3f; // the seconds it can endure before being killed by an attacker

    private List<EnemyMovement> _attackers = new List<EnemyMovement>();
    private float _enduranceTimer = 0f;

    private SpriteRenderer[] _spriteRenderers;
    private Color[] _startingColors;

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
        if (_attackers.Count > 0)
        {
            _enduranceTimer += Time.deltaTime;
            UpdateDamageVisual();
            if (_enduranceTimer >= endurance)
            {
                Debug.Log("EnemyAttackable destroyed by attackers!");
                Destroy(gameObject);
            }
        }
    }

    private void UpdateDamageVisual()
    {
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _spriteRenderers[i].color = Color.Lerp(Color.Lerp(_startingColors[i], Color.blue, 0.2f), Color.Lerp(_startingColors[i], Color.black, 0.6f), _enduranceTimer / endurance);
        }
    }

    public void AddAttacker(EnemyMovement enemy)
    {
        Debug.Log("Adding attacker = " + enemy.name);
        _attackers.Add(enemy);
    }

    public void RemoveAttacker(EnemyMovement enemy)
    {
        Debug.Log("Removing attacker = " + enemy.name);
        _attackers.Remove(enemy);

        if (_attackers.Count == 0)
        {
            _enduranceTimer = 0;
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                _spriteRenderers[i].color = _startingColors[i];
            }
        }
    }
}

