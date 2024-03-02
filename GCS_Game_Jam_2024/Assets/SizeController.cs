using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    public void ChangeSize(float amount)
    {
        transform.localScale += new Vector3(amount, amount);
    }
}
