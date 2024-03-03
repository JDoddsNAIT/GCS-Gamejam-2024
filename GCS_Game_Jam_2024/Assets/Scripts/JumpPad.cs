using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) {

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.GetComponent<PlayerMovement>().HitJumpPad();
            }
        }
    }
}
