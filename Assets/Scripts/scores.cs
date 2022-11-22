using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scores : MonoBehaviour
{
   public float points = 0;
   private GameObject score = null;

    void Start()
    {
        this.score = GameObject.Find("scores");
    }

    // Update is called once per frame
    void Update()
    {

        this.score.GetComponent<Text>().text= this.points.ToString("Score: " + points);
    }
}
