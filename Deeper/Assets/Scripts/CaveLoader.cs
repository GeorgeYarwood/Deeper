using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveLoader : MonoBehaviour
{
    //Array of caves we randomly select and load
    public GameObject[] caves = new GameObject[9];

    //The current cave we have loaded in
    public static GameObject currentCave;

    public Transform caveSpawn;

    public static bool loadNext;
    public static bool respawn;

    public string scr;
  
    int lastval;

    void Awake()
    {
        //Get player
        GameManager.Player = GameObject.FindGameObjectWithTag("Player");
        //Start by spawning in a cave




        //Load fist cave
        currentCave = Instantiate(caves[0], caveSpawn.position, Quaternion.identity);
        PlayerSpawn();
  

    }

    public void NextCave() 
    {
        
        int selection = Random.Range(1, caves.Length);

        //Select a different one from last time
        if(selection == lastval)
        {
            if(selection == caves.Length) 
            {
                selection -= 1;
            }
            else 
            {
                selection += 1;
            }
        }

            //Destroy old cave
            Destroy(currentCave);
            //Spawn new one
            currentCave = Instantiate(caves[selection], caveSpawn.position, Quaternion.identity);
            PlayerSpawn();

           

            lastval = selection;
        

        


    }


    void PlayerSpawn() 
    {
        //Disable component so we can move it
        (GameManager.Player.GetComponent(scr) as MonoBehaviour).enabled = false;
        //Spawn player at start of cave
        GameManager.Player.transform.position = currentCave.GetComponentInChildren<CaveSpawn>().transform.position;

        StartCoroutine(wait());


    }

    IEnumerator wait() 
    {
        yield return new WaitForSeconds(0.4f);
        //Enable again
        (GameManager.Player.GetComponent(scr) as MonoBehaviour).enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadNext) 
        {
            NextCave();
            loadNext = false;
        }

        if (respawn) 
        {
            //Destroy old cave
            Destroy(currentCave);


            //Load fist cave
            currentCave = Instantiate(caves[0], caveSpawn.position, Quaternion.identity);
            PlayerSpawn();
            respawn = false;
        }

    }
}
