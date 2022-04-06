using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLooseMenu : MonoBehaviour
{
    public CanvasGroup menu;
    public Button restartLevel;
    public Button backToMenu;
    
    private void Start()
    {
        menu.alpha = 0;
        
        GameManager.OnWin += OnGameWin;
        GameManager.OnLoose += OnGameLoose;
        
        restartLevel.onClick.AddListener(RestartLevel);
        backToMenu.onClick.AddListener(BackToMenu);
    }

    void OnGameWin()
    {
        menu.alpha = 1;
    }

    void OnGameLoose()
    {
        menu.alpha = 1;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
