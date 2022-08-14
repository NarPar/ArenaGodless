using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchAttack : MonoBehaviour
{
    [SerializeField] GameObject prefab = null;
    [SerializeField] float cooldown = 0.5f;
    [SerializeField] float range = 0.5f;
    [SerializeField] int damage = 1;

    private float _cooldownTimer = 0f;
    private PlayerMovement _movement;
    private Arm _arm;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _arm = GetComponentInChildren<Arm>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;

            if (_cooldownTimer <= 0f) _movement.SpeedMultiplier = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _cooldownTimer <= 0f)
        {
            _cooldownTimer = cooldown;
            _movement.SpeedMultiplier = 0.2f;
            DoAttack();
        }
    }

    private void DoAttack()
    {
        if (_arm.Weapon != null)
        {
            _arm.Weapon.DoAttack(gameObject, _movement.Direction);
        }
        else
        {
            DoScratchAttack();
        }
        
    }

    private void DoScratchAttack()
    {
        Vector3 pos = transform.position + (Vector3)(_movement.Direction * range);

        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.flipX = Random.value > 0.5f;
        sr.sortingOrder = -100 * Mathf.RoundToInt(pos.y) + 1;// + 10;

        // get attackables
        LayerMask mask = LayerMask.GetMask("Player", "Enemy");
        Vector3 start = transform.position + ((Vector3)_movement.Direction * 0.5f);
        Vector3 end = transform.position + (Vector3)(_movement.Direction * range);
        RaycastHit2D[] hits = Physics2D.CircleCastAll(start, 1f, _movement.Direction, range, mask);

        Debug.DrawLine(start, end, Color.red);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject == gameObject) continue;

                Debug.Log("Scratch: Hit " + hits[i].transform.name);

                Attackable attackable = hits[i].transform.GetComponent<Attackable>();
                attackable.Attack(damage, _movement.Direction);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 start = transform.position + ((Vector3)_movement.Direction * 0.5f);
        Vector3 end = transform.position + (Vector3)(_movement.Direction * range);
        Gizmos.DrawWireSphere(start, 1f);
        Gizmos.DrawWireSphere(end, 1f);
    }
}
