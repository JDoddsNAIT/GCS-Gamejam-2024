using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private GManager GameManager;
    private string name = "";
    void Start()
    {
        GameManager = GameObject.Find("GameManager").transform.GetComponent<GManager>();
        if (GameManager == null) { Debug.LogError("Game Manager is NULL!");  }

        GameManager.PlayerJoined(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetName(string newName)
    {
        name = newName;
    }
    public string GetPlayerName()
    {
        return name;
    }
}
