using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour
{
    public Text highscoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        highscoreTxt.text = "High Score: " + SaveSystem.ReadScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
