using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godzillalogic : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private Rigidbody2D playerBody;
    [SerializeField] private float playerDistance; // See the current distance from inspector

    [Header("Fireball")]
    [SerializeField] private Transform fireballPoint; // Shooting projectiles from this position
    [SerializeField] private GameObject projectile; // GodzillaFireball.prefab
    [SerializeField] private float fireballCooldown = 2f;
    private float fireballCooldownTimer;
    [SerializeField] private float fireballRange = 13f;
    [SerializeField] private float fireballCloseRange = 8f;

    [Header("Stomp")]
    [SerializeField] private float stompCooldown = 3.5f;
    private float stompCooldownTimer;
    [SerializeField] private float stompRange = 8f;
    [SerializeField] private int stompDamage = 1;
    [SerializeField] private float stompForce = 15f;

    [Header("Movements")]
    [SerializeField] private float maxDistance = 20f; // Start to follow the player at this distance
    [SerializeField] private float minDistance = 8f; // Stop following the player at this distance
    [SerializeField] private float movementSpeed = 3f;
    private int moveDirection;

    [Header ("SFX")]
    [SerializeField] private AudioClip stompSound;
    [SerializeField] private AudioClip shootSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        player = GameObject.Find("Player");
        fireballCooldownTimer += Time.deltaTime;
        stompCooldownTimer += Time.deltaTime;

        // Turn godzilla towards the player
        if (player.transform.position.x > transform.position.x)
        {
            // Parent-object overrides transform scale/rotate
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
        }

        // Set moveDirection towards the player
        if (transform.position.x < player.transform.position.x)
        {
            moveDirection = 1;
        }
        else
        {
            moveDirection = -1;
        }

        CheckIfStomp();
        CheckIfShoot();
        CheckIfMove();
    }

    private void CheckIfShoot()
    {
        player = GameObject.Find("Player");
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        if (playerDistance < fireballRange && fireballCooldownTimer > fireballCooldown)
        {
            if (playerDistance < fireballCloseRange)
            {
                fireballCooldown = 3f;
            }
            else
            {
                fireballCooldown = 1.5f;
            }
            anim.SetBool("walk", false);
            anim.SetBool("fire", true);

            // Shoot after "fire" animation
            Invoke("Shoot", 0.6f); // Wait 0.6s before calling Shoot()
            fireballCooldownTimer = 0;
        }
    }
    private void Shoot()
    {
        SoundManager.instance.PlaySound(shootSound);
        // "GodzillaFireball.prefab" dragged to "[SerializeField] private GameObject projectile"
        // Instantiate() clones this prefab to the game when shooting
        Instantiate(projectile, fireballPoint.position, Quaternion.identity);

        anim.SetBool("fire", false);
    }

    private void CheckIfStomp()
    {
        player = GameObject.Find("Player");
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        if (playerDistance <= stompRange && stompCooldownTimer > stompCooldown)
        {
            anim.SetBool("walk", false);
            anim.SetBool("stomp", true);

            stompCooldownTimer = 0;
            fireballCooldownTimer = 1;

            // Inflict damage when godzilla's leg hits the ground during "stomp" animation
            Invoke("Stomp", 0.6f); // Wait 0.6s before calling Stomp()
        }
    }
    private void Stomp()
    {
        player = GameObject.Find("Player");
        if (playerDistance <= stompRange && player.GetComponent<playermovements>().isGrounded())
        {
            SoundManager.instance.PlaySound(stompSound);
            Debug.Log("Stomped");
            player.GetComponent<playermovements>().goSliding(stompForce); // Could not apply stompForce in x-axis without sliding
            player.GetComponent<health>().TakeDamage(stompDamage);
            playerBody = player.GetComponent<Rigidbody2D>();

            playerBody.velocity = new Vector2(moveDirection * stompForce / 2, stompForce);
        }
        Invoke("stopStomp", 0.3f); // Wait until stomp animation ends
    }
    private void stopStomp()
    {
        anim.SetBool("stomp", false);
    }

    private void CheckIfMove()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if (playerDistance < maxDistance && playerDistance > minDistance && !anim.GetBool("fire"))
        {
            Move();
        }
        else if (anim.GetBool("walk"))
        {
            Debug.Log("stopWalk");
            anim.SetBool("walk", false);
        }
    }

    private void Move()
    {
        anim.SetBool("walk", true);

        transform.parent.position = new Vector2(transform.position.x + Time.deltaTime * moveDirection * movementSpeed,
        transform.parent.position.y);
    }
}