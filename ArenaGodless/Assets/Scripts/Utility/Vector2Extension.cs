using UnityEngine;

public static class Vector2Extension
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;

        float x = (cos * tx) - (sin * ty);
        float y = (sin * tx) + (cos * ty);

        return new Vector2(x, y);
    }
}