using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour
{
    public float points = 0;
    public float pills;

    [Header("SFX")]
    [SerializeField] private AudioClip pillSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player")) {
            // päivitetään scores 
            SoundManager.instance.PlaySound(pillSound);
            GameObject.Find("Main Camera").GetComponent<scores>().points += 1f;
                GameObject.Find("Main Camera").GetComponent<scores>().pills += 1f;
            Destroy(this.gameObject);
        }
    }
}