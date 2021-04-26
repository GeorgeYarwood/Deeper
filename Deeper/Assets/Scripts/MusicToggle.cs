using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggle : MonoBehaviour
{
    MusicManager aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = FindObjectOfType<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMute()
    {
        if (aud.gameObject.activeInHierarchy)
        {
            aud.gameObject.SetActive(false);


        }
        else
        {
            aud.gameObject.SetActive(true);
        }

    }
}
