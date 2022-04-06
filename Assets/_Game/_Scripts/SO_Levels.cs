using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels", fileName = "Levels")]
public class SO_Levels : ScriptableObject
{
    public List<SO_LevelConfiguration> levels = new List<SO_LevelConfiguration>();
    
    [Button]
    private void RegisterLevel()
    {
        levels = FindScriptableObject.GetLevels<SO_LevelConfiguration>();
    }
}