using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float kill = PlayerPrefs.GetFloat("kill");
        float pill = PlayerPrefs.GetFloat("pill");
        float allpoints = PlayerPrefs.GetFloat("all");
        
        GameObject.Find("kills").GetComponent<Text>().text = "Kills: " + kill.ToString("0");
        GameObject.Find("pills").GetComponent<Text>().text = "Pills: " + pill.ToString("0");
        GameObject.Find("scores").GetComponent<Text>().text = "Scores: " + allpoints.ToString("0");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
