#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public static class PlayerPrefsClearer
{
    [MenuItem("Tools/Clear Player Prefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs Cleared.");
    }
}

#endif