using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    private playermovements playermovements;
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        playermovements = GetComponent<playermovements>();
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
                playermovements.enabled = false;
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
