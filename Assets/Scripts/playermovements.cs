using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovements : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private bool pressjump;
    private laddermovements laddermovements;

    [SerializeField] private float speed;
    [SerializeField] private int jumpPower;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        // Grab references for rigidbody, boxcollider and animator from object
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        laddermovements = GetComponent<laddermovements>();
    }

    private void Update()
    {
        // Left + Right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip Player when moving left or right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(5, 5, 5);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 5, 5);

        // Jump
        if(Input.GetKey(KeyCode.Space) && isGrounded()) {
            Jump();
            pressjump = true;
        } else {
            pressjump = false;
        }

        // Sneak -> speed * 0.X
        if(Input.GetKey(KeyCode.LeftShift) && horizontalInput != 0 && isGrounded())
            body.velocity = new Vector2(horizontalInput * speed * 0.3f, body.velocity.y);

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("walk", horizontalInput != 0 && Input.GetKey(KeyCode.LeftShift));
        anim.SetBool("jump", pressjump);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("crouch", Input.GetKey(KeyCode.DownArrow) && isGrounded() && horizontalInput == 0);
        anim.SetBool("climb", laddermovements.isClimbing);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
    }

    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //public bool canAttack() {}
}