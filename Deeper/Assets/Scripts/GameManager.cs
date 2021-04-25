using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject fireImg;

    //Respawn menu items
    public GameObject respawnMenu;

    //For working out rotation direction
    Quaternion PrevPos;
    Quaternion NewPos;
    Quaternion ObjVelocity;

    //Count of how many caves we've been through
    public static int score = 0;

    //Display score
    public Text scoreTxt;

    //Last gameobject seen
    public GameObject lastobj;

    // Start is called before the first frame update
    void Start()
    {
        PrevPos = Player.transform.rotation;
        NewPos = Player.transform.rotation;

        //Set as not dead 
        dead = false;

        //Disable light at start
        lightobj.gameObject.SetActive(false);
        fireImg.SetActive(false);


    }

    //Fast phys sex
    void FixedUpdate()
    {
        //Work out diff between positions
        NewPos = Player.transform.rotation;  
        ObjVelocity = NewPos * Quaternion.Inverse(PrevPos);
        PrevPos = NewPos;  
    }

    public void Die()
    {
        //Show fail screen
        dead = true;
        respawnMenu.SetActive(true);
        //Disable fps controller temporarily
        (Player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;

        //Kill light
        LightSystem.lanternUp = false;

        lightobj.gameObject.SetActive(false);
        fireImg.SetActive(false);

        anim.SetTrigger("Down");

        //Unlock cursor
        Cursor.lockState = CursorLockMode.None;
    }

    public void Respawn()
    {
        //Reset player to start position
        //Spawn player at start of cave
        GameManager.Player.transform.position = CaveLoader.currentCave.GetComponentInChildren<CaveSpawn>().transform.position;

        //Reset light
        LightSystem.currentLight = LightSystem.baseLight;
        //Update bar
        LightSystem.updateVal = true;
        //Re enable fps controller
        (Player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;
        respawnMenu.SetActive(false);


        LightSystem.lanternUp = true;
        lightobj.gameObject.SetActive(true);
        fireImg.SetActive(true);

        anim.SetTrigger("Up");
        //Relock cursor
        Cursor.lockState = CursorLockMode.Locked;
        dead = false;
    }

    public void Exit()
    {
        //Application.Quit();
        SceneManager.LoadScene(0);
    }


    // Update is called once per frame
    void Update()
    {

        //Update score
        scoreTxt.text = score.ToString();

        //if we are not in the middle of a movement
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("moving")) 
        {
            if (ObjVelocity.y > 0.07)
            {
                anim.SetTrigger("Right");
            }
            else if (ObjVelocity.y < -0.07)
            {
                anim.SetTrigger("Left");

            }
        }


  


        if (dead)
        {
            Die();
        }

        if (Input.GetMouseButtonDown(0) && !dead)
        {
            LightSystem.lanternUp = true;
            lightobj.gameObject.SetActive(true);
            fireImg.SetActive(true);

            anim.SetTrigger("Up");
        }
        if (Input.GetMouseButtonUp(0) && LightSystem.lanternUp && !dead)
        {
            LightSystem.lanternUp = false;

            lightobj.gameObject.SetActive(false);
            fireImg.SetActive(false);

            anim.SetTrigger("Down");
        }

       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float dist = 15f;
        if (Physics.Raycast(ray, out hit, dist) && hit.transform.tag == "Oil")
        {
            //Show UI
            hit.transform.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text= "e";

            lastobj = hit.transform.gameObject;

            //Pick up if player presses e
            if (Input.GetKey("e") && !dead)
            {
                //Add random amount of light
                LightSystem.AddLight(Random.Range(55, 125));

                //Update bar
                LightSystem.updateVal = true;

                //Delete model
                Destroy(hit.transform.gameObject);
            }

        }
        else 
        {
            try 
            {
                //Hide UI
                lastobj.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = "";
            }
            catch 
            {
            
            }
           

        }


    }
}
