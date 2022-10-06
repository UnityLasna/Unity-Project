using UnityEngine;

public class octopusmonster : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<health>().TakeDamage(damage);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
