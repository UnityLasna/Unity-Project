using UnityEngine;

public class stomp : MonoBehaviour
{
    [SerializeField] private int damage; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy-head")
        {
            collision.GetComponentInParent<health>().TakeDamage(damage);
            GetComponentInParent<playermovements>().Jump();
        }
    }
}
