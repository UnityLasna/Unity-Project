using UnityEngine;

public class playershoot : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;

    [Header("SFX")]
    [SerializeField] private AudioClip shootingSound;

    private Animator anim;
    private playermovements playermovements;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playermovements = GetComponent<playermovements>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown && !anim.GetBool("slide"))
        {
            if (playermovements.isGrounded())
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                    RunShoot();
                else if (Input.GetKey(KeyCode.DownArrow))
                    CrouchShoot();
                else
                    StillShoot();
            }
            else
                JumpShoot();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void StillShoot()
    {
        SoundManager.instance.PlaySound(shootingSound);
        anim.SetTrigger("shoot");
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void RunShoot()
    {
        SoundManager.instance.PlaySound(shootingSound);
        anim.SetTrigger("run-shoot");
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void JumpShoot()
    {
        SoundManager.instance.PlaySound(shootingSound);
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void CrouchShoot()
    {
        SoundManager.instance.PlaySound(shootingSound);
        cooldownTimer = 0;

        Vector3 firePointCrouch = firePoint.position;
        firePointCrouch.y = firePointCrouch.y - 0.9f;

        bullets[FindBullet()].transform.position = firePointCrouch;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}

