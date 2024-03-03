using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GManager : MonoBehaviour
{
    public static GManager Instance { get; private set; }
    [SerializeField] private Player[] players;
    private int playerCount = 0;

    
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

    }

    public void PlayerJoined(Player newPlayer)
    {
        players[playerCount++] = newPlayer;

        StartGame();
    }

    public void StartGame()
    {
        if (playerCount >= 2)
        {
            SceneManager.LoadScene  (1);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.buildIndex);
        
        if (scene.buildIndex == 1)
        {
            Debug.Log("Level Scene Loaded!");
        }
    }

}
