using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevelFinal : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject finalPanel;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        finalPanel.SetActive(true);
    }
}
