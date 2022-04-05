using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static System.Action OnWin;
    public static System.Action OnLoose;

    private void Awake()
    {
        OnWin += OnGameWin;
        OnLoose += OnGameLoose;
    }

    void OnGameWin()
    {
        
    }

    void OnGameLoose()
    {
        
    }
}
