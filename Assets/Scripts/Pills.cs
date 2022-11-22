using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour
{
   // public float points = 0;
   private void OnTriggerEnter2D(Collider2D collision)
   {
    if (collision.name.Equals("Player")) {
        // päivitetään scores 
        GameObject.Find("Main Camera").GetComponent<scores>().points += 1f;
       // collision.GetComponent<health>().pill += 1f;
        Destroy(this.gameObject);
    }
   }
}
