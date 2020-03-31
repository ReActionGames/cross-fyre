using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameObjectExtensions {

    public static void SetLayer(this GameObject gameObject, string layerName, bool setChildrenLayer = false)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
        if (setChildrenLayer)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetLayer(layerName, true);
            }
        }
    }

    public static void SetLayer(this GameObject gameObject, int layerID, bool setChildrenLayer = false)
    {
        gameObject.layer = layerID;
        if (setChildrenLayer)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetLayer(layerID, true);
            }
        }
    }

    public static I[] FindMonoBehavioursOfInterface<I>(this MonoBehaviour monoBehaviour)
    {
        return Object.FindObjectsOfType<MonoBehaviour>().OfType<I>().ToArray();
    }
}
