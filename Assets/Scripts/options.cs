using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class options : MonoBehaviour
{
    public float volumevalue;

    // Start is called before the first frame update
    void Start()
    {
        volumevalue = GameObject.Find("SoundManager").GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(volumevalue);  
    }
}
