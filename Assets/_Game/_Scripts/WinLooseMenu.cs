using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLooseMenu : MonoBehaviour
{
    public CanvasGroup menu;
    public Button restartLevel;
    public Button backToMenu;
    public Button nextLevel;
    public TextMeshProUGUI winLooseText;
    
    private void Start()
    {
        menu.alpha = 0;
        nextLevel.gameObject.SetActive(false);
        
        GameManager.OnWin += OnGameWin;
        GameManager.OnLoose += OnGameLoose;
        
        restartLevel.onClick.AddListener(RestartLevel);
        backToMenu.onClick.AddListener(BackToMenu);
        nextLevel.onClick.AddListener(OnNextLevel);
        GetComponent<GraphicRaycaster>().enabled = false;
    }

    void OnNextLevel()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        Debug.Log("active scene : " + activeScene);
        Debug.Log("active scene length: " + activeScene[activeScene.Length - 1]);
        char index = activeScene[activeScene.Length - 1];
        Debug.Log("inedx : " + index);
        SceneManager.LoadScene("Level 0" + (int.Parse(index.ToString()) + 1));
    }

    void OnGameWin()
    {
        menu.alpha = 1;
        
        if(SceneManager.GetActiveScene().name != "Level 06")
            nextLevel.gameObject.SetActive(true);
        
        GetComponent<GraphicRaycaster>().enabled = true;
        winLooseText.text = "You win !";
    }

    void OnGameLoose()
    {
        nextLevel.gameObject.SetActive(false);
        menu.alpha = 1;
        GetComponent<GraphicRaycaster>().enabled = true;

        winLooseText.text = "You lost..";
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnDisable()
    {
        GameManager.OnWin -= OnGameWin;
        GameManager.OnLoose -= OnGameLoose;
    }
}
