using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float bulletLifeTime;

    private bool hit;
    private float direction;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > bulletLifeTime) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            hit = false;
        }
        else
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("impact");
        }

        if (collision.tag == "Enemy")
            collision.GetComponent<health>().TakeDamage(damage);
            
        if (collision.tag == "Boss")
            collision.GetComponent<bosshealth>().TakeDamage(damage);
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
