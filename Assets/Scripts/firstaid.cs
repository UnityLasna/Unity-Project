using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstaid : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player")) {
            Destroy(this.gameObject);
            
        }
    }
}
