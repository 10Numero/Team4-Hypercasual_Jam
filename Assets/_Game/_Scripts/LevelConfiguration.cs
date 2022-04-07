using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelConfiguration : MonoBehaviour
{
    [InlineButton("FindLevelConfiguration")]
    public SO_LevelConfiguration levelConfiguration;

    #if UNITY_EDITOR
    void FindLevelConfiguration()
    {
        levelConfiguration = FindScriptableObject.GetLevelConfiguration<SO_LevelConfiguration>(SceneManager.GetActiveScene().name);
    }
    void OnReset()
    {
        FindLevelConfiguration();
    }
#endif
}
