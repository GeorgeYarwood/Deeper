using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        CaveLoader.loadNext = true;
        //Add score
        GameManager.score += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
