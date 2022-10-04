using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    private playermovements playermovements;
    private crabpatrol crabpatrol;
    private crabmonster crabmonster;
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        playermovements = GetComponent<playermovements>();
        crabpatrol = GetComponentInParent<crabpatrol>();
        crabmonster = GetComponent<crabmonster>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            // Player Hurt
            anim.SetTrigger("hurt");
            //iframes
        }
        else
        {
            // Player Dead
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

                dead = true;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

}
