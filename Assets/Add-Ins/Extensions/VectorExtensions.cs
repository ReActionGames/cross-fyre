using UnityEngine;

public static class VectorExtensions
{
    /// <summary>
    /// Returns the same Vector3, except with the specified components replaced with the values passed in.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    /// <summary>
    /// Returns the same Vector2, except with the specified components replaced with the values passed in.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
    {
        return new Vector2(x ?? original.x, y ?? original.y);
    }

    /// <summary>
    /// Returns the same Vector3, except with the optional values that are passed in added to the original components.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 Shift(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(original.x + (x ?? 0), original.y + (y ?? 0), original.z + (z ?? 0));
    }

    /// <summary>
    /// Returns the same Vector2, except with the optional values that are passed in added to the original components.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector2 Shift(this Vector2 original, float? x = null, float? y = null)
    {
        return new Vector3(original.x + (x ?? 0), original.y + (y ?? 0));
    }

    public static Vector2 PolarToCartesian(float angle)
    {
        return PolarToCartesian(angle, Vector2.up);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="angle"></param>
    /// <param name="origin">The vector that represents 0 degrees</param>
    /// <returns></returns>
    public static Vector2 PolarToCartesian(float angle, Vector2 origin)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector2 point = rotation * origin;
        return point;
    }
}