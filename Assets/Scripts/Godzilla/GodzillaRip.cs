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
        SceneManager.LoadScene("GameOver");
    }
}
