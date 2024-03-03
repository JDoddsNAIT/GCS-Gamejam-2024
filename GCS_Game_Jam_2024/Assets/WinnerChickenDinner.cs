using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;

public class WinnerChickenDinner : MonoBehaviour
{
    private BoxCollider2D TdBox2D;
    [SerializeField]
    private TMP_Text _title;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            _title.gameObject.SetActive(true);
            _title.text = "Winner is ";
        }
    }
}
