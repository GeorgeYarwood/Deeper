using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //If we ded
    public static bool dead;

    //Hand animator
    public Animator anim;

    //So we can access our player
    public static GameObject Player;

    //The light we turn on and off
    public Light lightobj;

    //Respawn menu items
    public GameObject respawnMenu;



    // Start is called before the first frame update
    void Start()
    {
      

        //Set as not dead 
        dead = false;

        //Disable light at start
        lightobj.gameObject.SetActive(false);



    }


    public void Die()
    {
        //Show fail screen
        dead = true;
        respawnMenu.SetActive(true);

    }

    public void Respawn()
    {
        //Reset player to start position
        //Reset light
        LightSystem.currentLight = LightSystem.baseLight;

        dead = false;
    }



    // Update is called once per frame
    void Update()
    {

        if (dead)
        {
            Die();
        }

        if (Input.GetMouseButtonDown(0))
        {
            LightSystem.lanternUp = true;
            lightobj.gameObject.SetActive(true);
            anim.SetTrigger("Up");
        }
        if (Input.GetMouseButtonUp(0) && LightSystem.lanternUp)
        {
            LightSystem.lanternUp = false;

            lightobj.gameObject.SetActive(false);

            anim.SetTrigger("Down");
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Oil")
        {
            //Show UI
            hit.transform.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text= "e";

            //Pick up if player presses e
            if (Input.GetKey("e"))
            {
                //Add random amount of light
                LightSystem.AddLight(Random.Range(20, 100));

                //Update bar
                LightSystem.updateVal = true;

                //Delete model
                Destroy(hit.transform.gameObject);
            }

        }
        

    }
}
