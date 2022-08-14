using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 1f;
    [SerializeField] GameObject attackPrefab = null;
    [SerializeField] float rotateTime = 0f;
    [SerializeField] int damage = 100;
    [SerializeField] GameObject thrownPrefab = null;

    private float _rotate = 0f;

    public void DoAttack(GameObject source, Vector2 direction)
    {
        Debug.Log("Weapon: Doing Attack!");

        Vector3 pos = transform.position + (Vector3)(direction * range);
        float angle = Vector2.SignedAngle(Vector2.down, direction);
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        GameObject obj = Instantiate(attackPrefab, pos, rotation);
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.flipX = Random.value > 0.5f;
        sr.sortingOrder = -100 * Mathf.RoundToInt(pos.y) + 1;// + 10;

        _rotate = 360f;

        // get attackables
        LayerMask mask = LayerMask.GetMask("Player", "Enemy");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 2f, direction, range, mask);

        Debug.DrawLine(transform.position, transform.position + (Vector3)(direction * range), Color.red);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject == source) continue;

                Debug.Log("Weapon: Hit " + hits[i].transform.name);

                Attackable attackable = hits[i].transform.GetComponent<Attackable>();
                attackable.Attack(damage, direction);
            }
        }
    }

    public void Throw(GameObject source, Vector2 direction)
    {
        Vector3 pos = transform.position + (Vector3)(direction * range);

        GameObject obj = Instantiate(thrownPrefab, pos, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 1000f);

        Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), source.GetComponent<Collider2D>());

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_rotate > 0f)
        {
            _rotate -= 360f * (1f / rotateTime) * Time.deltaTime;
            _rotate = Mathf.Max(0f, _rotate);
            transform.localRotation = Quaternion.Euler(0f, 0f, _rotate);
        }
    }
}
