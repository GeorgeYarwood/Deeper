using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSystem : MonoBehaviour
{
    //Light value
    public static float currentLight;

    //Light starting value
    public static float baseLight = 500f;

    public Slider lightBar;

    //If the lantern is up
    public static bool lanternUp;


    public static bool updateVal;
    

    // Start is called before the first frame update
    void Start()
    {
        currentLight = baseLight;

        lightBar.value = currentLight;

    }


    public static void AddLight(float amount)
    {
        currentLight += amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (updateVal) 
        {
            lightBar.value = currentLight;
            updateVal = false;
        }

        //If we're not already dead and we can take light away
        if (!GameManager.dead && currentLight > 0 && lanternUp)
        {

            //Update the status bar
            lightBar.value = currentLight;
            currentLight -= 50 * Time.deltaTime;
        }
        else if(currentLight <= 0)
        {
            //Guess I'll die
            GameManager.dead = true;
        }

    }
}
