using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingAcid : MonoBehaviour
{
    private BoxCollider2D TdBox2D;
    public Rigidbody2D RB2D;
    [SerializeField] private GameObject _explodeEffectPrefab;
    private Vector3 _startPosition = Vector3.zero;
    //public AudioSource src;
    //public AudioClip sfx1;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //src.clip = sfx1;
        //src.Play();
        if (other.transform.gameObject.CompareTag("Player"))
        {
            other.transform.GetComponentInParent<Player>().KillPlayer();
        }
        Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
        transform.position = _startPosition;
        RB2D.velocity = Vector2.zero;
    }
}
