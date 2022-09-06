using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovements : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] private float speed;
    [SerializeField] private int jumpPower;

    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if(Input.GetKey(KeyCode.Space) && grounded)
            Jump();
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }
}