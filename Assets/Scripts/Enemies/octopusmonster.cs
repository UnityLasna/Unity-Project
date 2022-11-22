using UnityEngine;

public class octopusmonster : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GetComponent<health>().currentHealth > 0)
        {
            collision.GetComponent<health>().TakeDamage(damage);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<scores>().points += 1f;
    }
}
