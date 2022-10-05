using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMainMenuManager : MonoBehaviour
{
    GameManager gameManager;
    SaveSystem saveSystem;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        saveSystem = FindObjectOfType<SaveSystem>();
    }

    public void LoadSaveGame()
    {
        gameManager.LoadSaveGame();
    }

    public void LoadNextLevel()
    {
        gameManager.LoadNextLevel();
    }

    public void ResetSaveData()
    {
        saveSystem.ResetData();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
