using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string inputAxisX;
    public string jumpButton;
    public float speed;

    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent(out body))
        {
            body = this.gameObject.AddComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new(Input.GetAxis(inputAxisX), 0);
        transform.Translate(direction * speed * Time.deltaTime);

        
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown(jumpButton))
        {
            body.AddForce(transform.up);
        }
    }
}
