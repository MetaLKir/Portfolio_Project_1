using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.SaveData();
        gameManager.LoadNextLevel();
    }
}
