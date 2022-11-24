using UnityEngine;

public class goblin : MonoBehaviour
{
    [Header ("Common")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Rigidbody2D rb2d;

    [Header ("Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    private health playerHealth;

    [Header ("Sight")]
    [SerializeField] private float sightRange;
    [SerializeField] private float colliderDistance2;

    [Header ("Movements")]
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip attackSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        idleTimer += Time.deltaTime;

        if(PlayerInRange())
        {
            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack1");
                SoundManager.instance.PlaySound(attackSound);
            }
        }
        else if(PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            PlayerSpotted();
        }
        else
        {
            // Look around
            if(idleTimer > idleDuration)
            {
                transform.localScale = new Vector3(5, 5, 5);
                if (idleDuration*2 < idleTimer)
                {
                    idleTimer = 0;
                }
            }
            else
            {
                transform.localScale = new Vector3(-5, 5, 5);
            }
        }

        anim.SetBool("moving", PlayerInSight() && !PlayerInRange());
    }

    // Vision range
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * sightRange * transform.localScale.x * colliderDistance2,
        new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    // Attack when player is in attack range
    private bool PlayerInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
            playerHealth = hit.transform.GetComponent<health>();

        return hit.collider != null;
    }

    // Chases the player when in range
    private void PlayerSpotted()
    {
        if(transform.position.x < player.position.x)
        {
            // Move Right
            rb2d.velocity = new Vector2(speed, 0);
            idleTimer = idleDuration;
        }
        else if(transform.position.x > player.position.x)
        {
            // Move Left
            rb2d.velocity = new Vector2(-speed, 0);
            idleTimer = 0;
        }
    }

    // Attack range check
    private void OnDrawGizmos2()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // Vision range check
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * sightRange * transform.localScale.x * colliderDistance2,
        new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerInRange())
            playerHealth.TakeDamage(damage);
    }

    // Remove Goblin when killed
    private void Deactivate()
    {
        SoundManager.instance.PlaySound(deathSound);
        gameObject.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<scores>().points += 3f;
        GameObject.Find("Main Camera").GetComponent<scores>().kills += 1f;
    }

}