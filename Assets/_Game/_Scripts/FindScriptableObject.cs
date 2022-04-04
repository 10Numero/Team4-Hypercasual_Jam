#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class FindScriptableObject
{ 
    public static T GetLevelConfiguration<T>(string __configurationName) where T : ScriptableObject
    {
        var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");

        return guids.Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<T>)
            .FirstOrDefault(assets => assets.name == __configurationName);
    }
}
#endif
