using UnityEngine;

public class flyingmonster : MonoBehaviour
{
    [Header ("Common")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    private GameObject player;
    private Rigidbody2D rb2d;
    private Animator anim;

    [Header ("Attack")]
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    private health playerHealth;

    [Header ("Movement")]
    [SerializeField] private float speed;

    [Header ("Sight")]
    [SerializeField] private float sizeX;
    [SerializeField] private float sizeY;

    [Header ("SFX")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (player == null)
        {
            return;
        }
        if(PlayerInAttackRange())
        {
            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }
        else if(PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            Chase();
        }

        Flip();
        // rb2d.velocity = new Vector2(0, speed);
    }

    private void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        } else {
            transform.localScale = new Vector3(5, 5, 5);
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    // Vision range
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center,
        new Vector3(boxCollider.bounds.size.x * sizeX, boxCollider.bounds.size.y * sizeY, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    // Vision range check
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center,
        new Vector3(boxCollider.bounds.size.x * sizeX, boxCollider.bounds.size.y * sizeY, boxCollider.bounds.size.z));
    }

    // Attack when player is in attack range
    private bool PlayerInAttackRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
            playerHealth = hit.transform.GetComponent<health>();

        return hit.collider != null;
    }

    // Attack range check
    private void OnDrawGizmos2()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // Damage player
    private void DamagePlayer()
    {
        if(PlayerInAttackRange())
            playerHealth.TakeDamage(damage);
    }

    // Remove mob when killed
    private void Deactivate()
    {
        SoundManager.instance.PlaySound(deathSound);
        gameObject.SetActive(false);
    }
}
