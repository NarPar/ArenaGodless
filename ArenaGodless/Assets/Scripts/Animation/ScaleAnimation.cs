using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] float minScale = 0.2f;
    [SerializeField] float maxScale = 0.6f;
    [SerializeField] float time = 0.5f;
    [SerializeField] bool fastOut = true;

    private float _t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _t += Time.deltaTime;
        float progress = Mathf.Min(_t / time, 1f);
        float styledProgress = fastOut ? Mathf.Pow(progress, 3f) : Mathf.Pow(progress, 0.3f);
        float scale = Mathf.Lerp(maxScale, minScale, styledProgress);

        transform.localScale = new Vector3(scale, scale, 1f);

        if (progress >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
