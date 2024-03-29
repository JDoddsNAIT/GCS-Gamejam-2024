using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddPlayer : MonoBehaviour
{
    //Intitalize Public Variables
    public int PlayerCount = 0;
    public int LastPlayerCount = 0;
    public int Pnum = 1;
    public GameObject[] PlayerArray;
    private GManager GameManager = null;

    public void Start()
    {
        PlayerCount = 0;
        LastPlayerCount = 0;
        Pnum = 1;

        GameManager = GameObject.Find("GameManager").transform.GetComponent<GManager>();
        if (GameManager == null) { Debug.LogError("Game Manager is NULL!");  }

        GameManager.PlayerJoin += OnPlayerJoin;
    }
    public void Update()
    {

        //If a player was added to the game
        if (LastPlayerCount < PlayerCount && PlayerCount <= 4)
        {
            PlayerCount -= 1;
            PlayerArray[PlayerCount].SetActive(true);
            if(PlayerCount == 0)
            {
                PlayerArray[4].SetActive(true);
            }
            PlayerCount += 1;
            Debug.Log("P" + Pnum + " has been added to the game");
            Pnum += 1;
        }

        //Update if player was added
        if (PlayerCount > LastPlayerCount && PlayerCount <= 4)
        {
            LastPlayerCount = PlayerCount;
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnPlayerJoin()
    {
        PlayerCount++;
    }
}

// If a player joins
// Add their Charcater and show them in Scene
// 
