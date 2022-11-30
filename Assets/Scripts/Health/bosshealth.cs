using UnityEngine;
using System.Collections;

public class bosshealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 10;
    [SerializeField] public int currentHealth;
    [SerializeField] private float invulnerabilityDuration = 3; // Be invulnerable after taking damage

    private bool takeDamage = true;
    private bool dead;
    private godzillalogic godzillalogic;
    private SpriteRenderer spriteRend;
    private BoxCollider2D boxCollider;
    private GameObject deathObj;

    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRend = GetComponentInParent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        godzillalogic = GetComponent<godzillalogic>();

        deathObj = transform.GetChild(1).gameObject; // Child object for death-animation
        deathObj.GetComponent<SpriteRenderer>().enabled = false; // Disable until death
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            if (takeDamage)
            {
                StartCoroutine(Invulnerability()); // Turn red while invulnerable
                currentHealth = currentHealth - damage;

                Debug.Log("Godzilla current health: " + currentHealth);
            }
            else
            {
                Debug.Log("Godzilla is invulnerable");
            }
        }
        else
        {
            if (!dead)
            {
                dead = true;

                // Disable godzilla
                spriteRend.enabled = false;
                boxCollider.enabled = false;
                godzillalogic.enabled = false;

                // Play death-animation
                deathObj.GetComponent<SpriteRenderer>().enabled = true;
                deathObj.GetComponent<Animator>().SetTrigger("death");
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        takeDamage = false;

        spriteRend.color = new Color(1, 0, 0, 1f);
        yield return new WaitForSeconds(invulnerabilityDuration);
        spriteRend.color = Color.white;

        takeDamage = true;
    }
}