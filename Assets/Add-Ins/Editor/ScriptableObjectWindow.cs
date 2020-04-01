using System;
using System.Linq;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

internal class EndNameEdit : EndNameEditAction
{
    #region implemented abstract members of EndNameEditAction

    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        AssetDatabase.CreateAsset(EditorUtility.InstanceIDToObject(instanceId),
            AssetDatabase.GenerateUniqueAssetPath(pathName));
    }

    #endregion
}

/// <summary>
/// Scriptable object window.
/// </summary>
public class ScriptableObjectWindow : EditorWindow
{
    private int selectedIndex;
    private static string[] _names;

    private static Type[] _types;

    private static Type[] Types
    {
        get => _types;
        set
        {
            _types = value;
            _names = _types.Select(t => t.FullName).ToArray();
        }
    }

    public static void Init(Type[] scriptableObjects)
    {
        Types = scriptableObjects;

        var window = GetWindow<ScriptableObjectWindow>(true, "Create a new ScriptableObject", true);
        window.ShowPopup();
    }

    public void OnGUI()
    {
        GUILayout.Label("ScriptableObject Class");
        selectedIndex = EditorGUILayout.Popup(selectedIndex, _names);

        if (GUILayout.Button("Create"))
        {
            var asset = CreateInstance(_types[selectedIndex]);
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                asset.GetInstanceID(),
                CreateInstance<EndNameEdit>(),
                $"{_names[selectedIndex]}.asset",
                AssetPreview.GetMiniThumbnail(asset),
                null);

            Close();
        }
    }
}