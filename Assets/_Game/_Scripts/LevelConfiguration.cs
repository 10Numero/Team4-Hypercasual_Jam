using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelConfiguration : MonoBehaviour
{
    [InlineButton("FindLevelConfiguration")]
    public SO_LevelConfiguration levelConfiguration;

    void FindLevelConfiguration()
    {
        levelConfiguration = FindScriptableObject.GetLevelConfiguration<SO_LevelConfiguration>(SceneManager.GetActiveScene().name);
    }

    void OnReset()
    {
        FindLevelConfiguration();
    }
}
