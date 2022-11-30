using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    private playermovements playermovements;
    private Animator anim;
    private bool dead;
    public float kills;


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

    [Header("SFX")]
    [SerializeField] private AudioClip deathSoundPlayer;
    [SerializeField] private AudioClip hurtSoundPlayer;
    [SerializeField] private AudioClip deathSoundEnemy;

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
            SoundManager.instance.PlaySound(hurtSoundPlayer);
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
                if (playermovements != null)
                {
                    playermovements.enabled = false;
                    SoundManager.instance.PlaySound(deathSoundPlayer);
                    Invoke("GameOver", 0.6f);
                }
                // Enemy
                if (crabpatrol != null)
                {
                    crabpatrol.enabled = false;
                    
                    SoundManager.instance.PlaySound(deathSoundEnemy);
                }

                if (crabmonster != null)
                {
                    crabmonster.enabled = false;
                    
                    SoundManager.instance.PlaySound(deathSoundEnemy);
                }

                if (octopusmonster != null)
                {
                    octopusmonster.enabled = false;
                    
                    SoundManager.instance.PlaySound(deathSoundEnemy);
                }   

                dead = true;
            }
        }
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

    private void GameOver()
    {

        PlayerPrefs.SetFloat("kill", GameObject.Find("Main Camera").GetComponent<scores>().kills); 
        PlayerPrefs.SetFloat("pill", GameObject.Find("Main Camera").GetComponent<scores>().pills);
        PlayerPrefs.SetFloat("all", GameObject.Find("Main Camera").GetComponent<scores>().points);

        SceneManager.LoadScene("GameOver");
        Debug.Log(GameObject.Find("Main Camera").GetComponent<scores>().kills);
        Debug.Log(GameObject.Find("Main Camera").GetComponent<scores>().pills);
        Debug.Log(GameObject.Find("Main Camera").GetComponent<scores>().points);

    }

}