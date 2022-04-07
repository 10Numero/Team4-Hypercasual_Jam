using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public SO_Levels levels;
    public Button buttonLevelPrefab;
    public Transform[] levelContainer;
    public Button level;
    public Transform menuContainer;
    
    private void Awake()
    {
        for(var i = 0; i < levels.levels.Count; i++)
        {
            var index = i;
            var btn = Instantiate(buttonLevelPrefab, levelContainer[0]);
            btn.onClick.AddListener(() => StartLevel(index));
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levels.levels[i].levelName;
        }

        foreach (Transform tr in levelContainer)
        {
            tr.gameObject.SetActive(false);
            
        }
        
        menuContainer.gameObject.SetActive(true);
        
        level.onClick.AddListener(OpenLevel);
    }

    void OpenLevel()
    {
        foreach (Transform tr in levelContainer)
        {
            tr.gameObject.SetActive(true);
            
        }
        menuContainer.gameObject.SetActive(false);
    }

    void StartLevel(int __index)
    {
        SceneManager.LoadScene(levels.levels[__index].levelSceneName);
    }
}
