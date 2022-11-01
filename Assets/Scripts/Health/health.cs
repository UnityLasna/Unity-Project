using UnityEngine;
using System.Collections;

public class health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    private playermovements playermovements;
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Enemys")]
    private crabpatrol crabpatrol;
    private crabmonster crabmonster;
    private octopusmonster octopusmonster;
    //
    private tantacle tantacle;
    

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        playermovements = GetComponent<playermovements>();
        crabpatrol = GetComponentInParent<crabpatrol>();
        crabmonster = GetComponent<crabmonster>();
        octopusmonster = GetComponent<octopusmonster>();
        //
        tantacle = GetComponent<tantacle>();
       
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            // Player Hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            //iframes
        }
        else
        {
            // Creature Dead
            if(!dead)
            {
                anim.SetTrigger("death");

                // Player
                if(playermovements != null)
                    playermovements.enabled = false;

                // Enemy
                if(crabpatrol != null)
                    crabpatrol.enabled = false;

                if(crabmonster != null)
                    crabmonster.enabled = false;

                if(octopusmonster != null)
                    octopusmonster.enabled = false;

               

                dead = true;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);

    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

}