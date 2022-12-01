using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodzillaRip : MonoBehaviour
{
    // Remove when killed
    private void Deactivate()
    {
        gameObject.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<scores>().points += 10f;
        GameObject.Find("Main Camera").GetComponent<scores>().kills += 1f;
        SceneManager.LoadScene("GameOver");
    }
}
