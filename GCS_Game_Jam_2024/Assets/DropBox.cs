using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DropBox : MonoBehaviour
{
    private BoxCollider2D TdBox2D;
    public Transform target;
    public Rigidbody2D RB2D;
    public float t;
    public float speed;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            Debug.Log("Yellow");
        }
        Debug.Log("It's a me Mario");
        Vector2 a = transform.position;
        Vector2 b = target.position;
    }
}
