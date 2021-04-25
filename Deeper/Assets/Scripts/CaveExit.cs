using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveExit : MonoBehaviour
{
    bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {

        if (isColliding) return;
        isColliding = true;
        CaveLoader.loadNext = true;
        //Add score
        GameManager.score += 1;

    

    }

   

    // Update is called once per frame
    void Update()
    {
        isColliding = false;


    }
}
