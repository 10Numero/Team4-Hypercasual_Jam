using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public SO_Levels levels;
    public Button buttonLevelPrefab;
    public Transform levelContainer;
    
    private void Awake()
    {
        for(var i = 0; i < levels.levels.Count; i++)
        {
            var index = i;
            var btn = Instantiate(buttonLevelPrefab, levelContainer);
            btn.onClick.AddListener(() => StartLevel(index));
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levels.levels[i].levelName;
        }
    }

    void StartLevel(int __index)
    {
        SceneManager.LoadScene(levels.levels[__index].levelSceneName);
    }
}
