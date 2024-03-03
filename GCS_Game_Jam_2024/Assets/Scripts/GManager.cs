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
    [SerializeField] private Transform[] alivePlayers;
    private int playerCount = 0;
    private WaitForSeconds _gameResetDelay = new WaitForSeconds(5);
    public event Action PlayerJoin;

    private Vector3[] _playerSpawnPositions = new Vector3[4];
    private FollowObject _cameraController;
    private bool _gameStarted = false;
    
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

    private void Update()
    {
        if (_gameStarted)
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (!players[i].IsDead() &&
                    Math.Abs(players[i].GetControllerPosX() - _cameraController.transform.position.x) > 9.0f)
                {
                    players[i].KillPlayer();
                }
            }

            int deadPlayerCount = 0;
            for (int i = 0; i < playerCount; i++)
            {
                if (players[i].IsDead())
                {
                    deadPlayerCount++;
                }
            }

            if (deadPlayerCount == playerCount)
            {
                StartCoroutine(ResetGame());
            }
        }
    }

    public void PlayerJoined(Player newPlayer)
    {
        players[playerCount++] = newPlayer;
        newPlayer.SetName("Player " + playerCount);
        PlayerJoin();
    }

    public void StartGame()
    {
        if (playerCount >= 1)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void GameOver(bool win)
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].KillPlayer();
        }

        // Stop Camera Moving
        StartCoroutine(ResetGame());
    }

    public void RestartGame()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].ResetPlayer(_playerSpawnPositions[i]);
        }

        _cameraController.ResetPosition();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene #" + scene.buildIndex + " Loaded" );
        
        if (scene.buildIndex == 1 || scene.buildIndex == 2)
        {
            // Get The Scene Spawnpoints 
            Transform spawnPointParent = GameObject.Find("PlayerSpawnPoints").transform;
            if (spawnPointParent == null) { Debug.LogError("Can't find player spawn points for this scene!");  }

            // Get Camera Controller
            _cameraController = GameObject.Find("Camera").GetComponent<FollowObject>();

            for (int i = 0; i < playerCount; i++)
            {
                _playerSpawnPositions[i] = spawnPointParent.GetChild(i).position;
                players[i].ResetPlayer(_playerSpawnPositions[i]);
            }

            _gameStarted = true;
        }
    }

    IEnumerator ResetGame()
    {
        yield return _gameResetDelay;
        ResetGame();
    }

}
