using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instanceSaveSystem;
    
    public string playerHealthKey = "Player health", sceneKey = "Scene index", saveExistKey = "Save present";

    public LoadedData LoadedData { get; private set; }

    public UnityEvent<bool> OnDataLoadedResult;

    //---------------------------------------------------//
    private void Awake()
    {
        if (instanceSaveSystem != null)
        {
            Destroy(gameObject);
            return;
        }
        instanceSaveSystem = this;
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        bool result = LoadData();
        OnDataLoadedResult?.Invoke(result);
    }
    //---------------------------------------------------//
    public void SaveData(int sceneIndex, int playerHealth)
    {
        if (LoadedData == null)
            LoadedData = new LoadedData();

        LoadedData.playerHealth = playerHealth;
        LoadedData.sceneIndex = sceneIndex;
        PlayerPrefs.SetInt(playerHealthKey, playerHealth);
        PlayerPrefs.SetInt(sceneKey, sceneIndex);
        PlayerPrefs.SetInt(saveExistKey, 1);
    }

    public bool LoadData()
    {
        if (PlayerPrefs.GetInt(saveExistKey) == 1)
        {
            LoadedData = new LoadedData();
            LoadedData.playerHealth = PlayerPrefs.GetInt(playerHealthKey);
            LoadedData.sceneIndex = PlayerPrefs.GetInt(sceneKey);
            return true;

        }
        return false;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey(playerHealthKey);
        PlayerPrefs.DeleteKey(sceneKey);
        PlayerPrefs.DeleteKey(saveExistKey);
        LoadedData = null;
    }
}
//---------------------------------------------------//
public class LoadedData
{
    public int playerHealth = -1;
    public int sceneIndex = -1;
}
