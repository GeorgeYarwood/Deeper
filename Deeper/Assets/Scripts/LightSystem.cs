using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public int drainrate;

    public GameObject infImg;
    

    // Start is called before the first frame update
    void Start()
    {
        currentLight = baseLight;

        lightBar.value = currentLight;

        LightSystem loadedLightSys = new LightSystem();
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/lightsys"), loadedLightSys);

        drainrate = loadedLightSys.drainrate;

        if(drainrate == 0) 
        {
            infImg.SetActive(true);
        }
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
            currentLight -= drainrate * Time.deltaTime;
        }
        else if(currentLight <= 0)
        {
            //Guess I'll die
            GameManager.dead = true;
        }

    }
}
