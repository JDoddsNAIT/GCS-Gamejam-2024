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

    public event Action PlayerJoin;

    
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

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    public void PlayerJoined(Player newPlayer)
    {
        players[playerCount++] = newPlayer;
        newPlayer.gameObject.transform.position = new Vector3(-550, 160, 0);
        PlayerJoin();
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
            for (int i = 0; i < playerCount; i++)
            {
                Debug.Log("!!!");
                players[i].gameObject.transform.position = new Vector3(0, 0, 0);
            }
        }
    }

}
