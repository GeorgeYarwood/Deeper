using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveExit : MonoBehaviour
{

    bool canRegister;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!canRegister && other.tag == "Player" )
        {
            canRegister = true;
            CaveLoader.loadNext = true;
            //Add score
            GameManager.score += 1;
            StartCoroutine(wait());

            //Give a little bit of light back
            LightSystem.currentLight += 20;

            if(LightSystem.currentLight > 500f) 
            {
                LightSystem.currentLight = 500f;
            }
            LightSystem.updateVal = true;
        }
     

    

    }

   IEnumerator wait() 
    {
        yield return new WaitForEndOfFrame();
        canRegister = false; 
    }

    // Update is called once per frame
    void Update()
    {


    }
}
