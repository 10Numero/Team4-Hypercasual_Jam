using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static System.Action OnWin;
    public static System.Action OnLoose;
    public static System.Action OnGameStart;

    private void Awake()
    {
        OnWin += OnGameWin;
        OnLoose += OnGameLoose;

        Application.targetFrameRate = 60;
    }

    void OnGameWin()
    {
        
    }

    void OnGameLoose()
    {
        
    }

    private void OnDisable()
    {
        OnWin -= OnWin;
        OnLoose -= OnLoose;
    }
}
