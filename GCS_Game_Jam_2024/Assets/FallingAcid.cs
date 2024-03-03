using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingAcid : MonoBehaviour
{
    private BoxCollider2D TdBox2D;
    public Transform target;
    public Rigidbody2D RB2D;
    //public AudioSource src;
    //public AudioClip sfx1;


    public void OnTriggerEnter2D(Collider2D other)
    {
        //src.clip = sfx1;
        //src.Play();
        if (other.transform.gameObject.CompareTag("Player"))
        {
            Debug.Log("Yellow");
        }
        transform.position = target.position;
        RB2D.velocity = Vector2.zero;
    }
}
