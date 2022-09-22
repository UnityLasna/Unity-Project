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

        // crouch shoot
        // Logiikka toimi, kun nostin "kumarassa ampumisen" "paikoillaan ampumisen" yläpuolelle.
        else if(Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow) && cooldownTimer > attackCooldown && playermovements.isGrounded())
            CrouchShoot();

        // still shoot
        else if(Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown && playermovements.isGrounded())
            StillShoot();

        // jump shoot
        else if(Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown)
            JumpShoot();

        cooldownTimer += Time.deltaTime;
    }

    private void StillShoot()
    {
        // Track in console
        Debug.Log("Entered StillShoot()");

        anim.SetTrigger("shoot");
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void RunShoot()
    {
        // Track in console
        Debug.Log("Entered RunShoot()");

        anim.SetTrigger("run-shoot");
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void JumpShoot()
    {
        // Track in console
        Debug.Log("Entered JumpShoot()");

        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = firePoint.position;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void CrouchShoot()
    {
        // Track in console
        Debug.Log("Entered CrouchShoot()");
        //Debug.Log(firePoint.position.y);

        anim.SetTrigger("crouch-shoot");
        cooldownTimer = 0;

        Vector3 firePointCrouch = firePoint.position;
        firePointCrouch.y = firePointCrouch.y -0.9f;
        Debug.Log(firePointCrouch.y);
        //(firePoint.position.x, firePoint.position.y - 0.03f, firePoint.position.z);

        //firePoint.position.y = new Vector3 (firePoint.position.x, firePoint.position.y - 0.03f, firePoint.position.z);

        bullets[FindBullet()].transform.position = firePointCrouch;
        bullets[FindBullet()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    

        //firePoint.position.y = firePoint.position.y - 0.03f;
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

