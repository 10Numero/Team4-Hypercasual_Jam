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
    public TextMeshProUGUI winLooseText;
    
    private void Start()
    {
        menu.alpha = 0;
        
        GameManager.OnWin += OnGameWin;
        GameManager.OnLoose += OnGameLoose;
        
        restartLevel.onClick.AddListener(RestartLevel);
        backToMenu.onClick.AddListener(BackToMenu);
        GetComponent<GraphicRaycaster>().enabled = false;
    }

    void OnGameWin()
    {
        menu.alpha = 1;
        GetComponent<GraphicRaycaster>().enabled = true;
        winLooseText.text = "You win !";
    }

    void OnGameLoose()
    {
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
