using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    float volume = 1f;
    public AudioSource aud;
    public Slider volSlid;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        //There can only be one
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        if(sources.Length > 1) 
        {
            Destroy(sources[0]);
        }

       
    }

    void Update()
    {

        volume = volSlid.value;

        aud.volume = volume;
    }


  
}
