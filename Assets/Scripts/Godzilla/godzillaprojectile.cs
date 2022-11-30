using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godzillaprojectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 6f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float projectileLifeTime = 5f; // Seconds

    private Rigidbody2D rb2D;
    private GameObject target;
    private Vector2 moveDirection;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        moveDirection = (target.transform.position - transform.position).normalized * projectileSpeed;
        rb2D.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, projectileLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Boss")
        {
            // Can't hit yourself
        }
        else
        {
            anim.SetTrigger("impact");
            rb2D.velocity = new Vector2(0, 0);
            Destroy(gameObject, 0.6f); // Destroy Fireball gameObject after explosion animation
            
            if (col.tag == "Player")
            {
                col.GetComponent<health>().TakeDamage(damage);
            }
        }
    }
}
