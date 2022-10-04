using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scores : MonoBehaviour
{
   public float pills = 0;
   private GameObject score = null;

    void Start()
    {
        this.score = GameObject.Find("score");
    }

    // Update is called once per frame
    void Update()
    {

        this.score.GetComponent<Text>().text= this.pills.ToString("Score: " + pills);
    }
}
