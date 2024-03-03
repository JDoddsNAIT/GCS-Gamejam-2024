using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDamage : MonoBehaviour
{
    private BoxCollider2D TdBox2D;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            other.transform.GetComponentInParent<Player>().KillPlayer();
        }
    }
}
