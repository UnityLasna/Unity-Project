using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
    if (collision.name.Equals("Player")) {
        // päivitetään scores 
        GameObject.Find("score").GetComponent<scores>().pills += 1f;
        Destroy(this.gameObject);
    }
   }
}
