using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            PlayerPrefs.SetFloat("kill", GameObject.Find("Main Camera").GetComponent<scores>().kills);
            PlayerPrefs.SetFloat("pill", GameObject.Find("Main Camera").GetComponent<scores>().pills);
            PlayerPrefs.SetFloat("all", GameObject.Find("Main Camera").GetComponent<scores>().points);
            SceneManager.LoadScene("GameOver");
        }
    }
}
