using UnityEngine;

public class playershoot : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;

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
        // running shoot left
        if(Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown && playermovements.isGrounded() && Input.GetKey(KeyCode.LeftArrow))
            RunShoot();
        // running shoot right
        else if(Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown && playermovements.isGrounded() && Input.GetKey(KeyCode.RightArrow))
            RunShoot();

        // still shoot
        else if(Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown && playermovements.isGrounded())
            StillShoot();

        // jump shoot
        else if(Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown)
            JumpShoot();

        // crouch shoot
        else if(Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow) && cooldownTimer > attackCooldown && playermovements.isGrounded())
            CrouchShoot();

        cooldownTimer += Time.deltaTime;
    }

    private void StillShoot()
    {
        anim.SetTrigger("shoot");
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void RunShoot()
    {
        anim.SetTrigger("run-shoot");
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void JumpShoot()
    {
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void CrouchShoot()
    {
        anim.SetTrigger("crouch-shoot");
        cooldownTimer = 0;
    }

    private int FindBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if(!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}

