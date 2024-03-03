using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine.SceneManagement;

public class WinnerChickenDinner : MonoBehaviour
{
    private BoxCollider2D TdBox2D;
    [SerializeField]
    private TMP_Text _title;
    private GManager GameManager = null;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            _title.gameObject.SetActive(true);
            _title.text = "Winner is " + other.gameObject.transform.GetComponent<Player>().GetPlayerName();
            //GameManager = GameObject.Find("GameManager").transform.GetComponent<GManager>();
            //if (GameManager == null) { Debug.LogError("Game Manager is NULL!"); }

        }
    }
}
