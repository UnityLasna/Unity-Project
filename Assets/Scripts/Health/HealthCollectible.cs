using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    [Header("SFX")]
    [SerializeField] private AudioClip healthSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            SoundManager.instance.PlaySound(healthSound);
            collision.GetComponent<health>().AddHealth(healthValue);
            Destroy(this.gameObject);
        }
    }
}
