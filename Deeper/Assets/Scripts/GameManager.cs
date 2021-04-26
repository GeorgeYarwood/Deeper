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

    //Audio for sfx
    public AudioSource sfx;

    public AudioClip[] clips = new AudioClip[7];

    //Tutorial go
    public GameObject tutorial;

    //Pause menu
    public GameObject pauseMenu;

    [SerializeField]
    public int savedScore;

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

        Cursor.visible = true;

        savedScore = score;

        //If we got a high score
        if(score > SaveSystem.ReadScore()) 
        {
            //Save score
            SaveSystem.SaveScore();
        }

      
    }

    public void Respawn()
    {
        //Reset player to start position and load new cave
        CaveLoader.respawn = true;
        

        //Reset light
        LightSystem.currentLight = LightSystem.baseLight;
        //Update bar
        LightSystem.updateVal = true;
        //Reset score
        score = 0;

        //Re enable fps controller
        (Player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;
        respawnMenu.SetActive(false);


        LightSystem.lanternUp = true;
        lightobj.gameObject.SetActive(true);
        fireImg.SetActive(true);

        anim.SetTrigger("Up");
        //Relock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dead = false;
    }

    public void Exit()
    {
        savedScore = score;

        //If we got a high score
        if (score > SaveSystem.ReadScore())
        {
            //Save score
            SaveSystem.SaveScore();
        }

        //Application.Quit();
        SceneManager.LoadScene(0);
    }


    public void FreeCursor() 
    {
        //Re enable fps controller
        (Player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    public void LoadMenu() 
    {
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
            if (ObjVelocity.y > 0.05)
            {
                anim.SetTrigger("Right");
                sfx.clip = clips[Random.Range(0,clips.Length)];
                sfx.Play();
            }
            else if (ObjVelocity.y < -0.05)
            {
                sfx.clip = clips[Random.Range(0, clips.Length)];
                sfx.Play();
                anim.SetTrigger("Left");

            }
        }


  


        if (dead)
        {
            Die();
        }

        if (Input.GetKey(KeyCode.Escape)) 
        {   
            //Show pause menu and unlock cursor
            pauseMenu.SetActive(true);

            //Re enable fps controller
            (Player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
            Cursor.lockState = CursorLockMode.None;

            Cursor.visible = true;



        }

        if (Input.GetMouseButtonDown(0) && !dead && !pauseMenu.activeInHierarchy)
        {
            if (tutorial.activeInHierarchy) 
            {
                tutorial.SetActive(false);
            }

            LightSystem.lanternUp = true;
            lightobj.gameObject.SetActive(true);
            fireImg.SetActive(true);

            sfx.clip = clips[Random.Range(0, clips.Length)];
            sfx.Play();
            anim.SetTrigger("Up");
      }
        if (Input.GetMouseButtonUp(0) && LightSystem.lanternUp && !dead && !pauseMenu.activeInHierarchy)
        {
            LightSystem.lanternUp = false;
            sfx.clip = clips[Random.Range(0, clips.Length)];
            sfx.Play();
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

                //Cap at max value
                if(LightSystem.currentLight > LightSystem.baseLight)
                {
                    LightSystem.currentLight = LightSystem.baseLight;
                }
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
