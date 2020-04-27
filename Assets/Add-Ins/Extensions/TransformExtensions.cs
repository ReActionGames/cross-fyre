using UnityEngine;

public static class TransformExtensions
{
    public static Transform DestroyChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
        return transform;
    }
}