using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instanceGameManager;
    
    public GameObject player;
    public SaveSystem saveSystem;

    //-----------------------------------------//
    private void Awake()
    {
        if (instanceGameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        instanceGameManager = this;
        
        SceneManager.sceneLoaded += Initialize;
        DontDestroyOnLoad(gameObject);
    }
    //-----------------------------------------//
    void Initialize(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("Loaded GameManager");

        var playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
            player = playerInput.gameObject;

        saveSystem = FindObjectOfType<SaveSystem>();
        if (player != null && saveSystem.LoadedData != null)
        {
            var hitpoints = player.GetComponentInChildren<Hitpoints>();
            hitpoints.HealthCurrent = saveSystem.LoadedData.playerHealth;
        }
    }

    public void LoadSaveGame()
    {     
        if (saveSystem.LoadedData != null)
        {
            SceneManager.LoadScene(saveSystem.LoadedData.sceneIndex);
            return;
        }
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SaveData()
    {
        if (player != null)
        {
            saveSystem.SaveData(
                SceneManager.GetActiveScene().buildIndex + 1, 
                player.GetComponentInChildren<Hitpoints>().HealthCurrent);
        }
    }
}
