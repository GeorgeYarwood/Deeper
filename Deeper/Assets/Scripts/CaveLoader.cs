using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveLoader : MonoBehaviour
{
    //Array of caves we randomly select and load
    public GameObject[] caves = new GameObject[3];

    //The current cave we have loaded in
    public static GameObject currentCave;

    public Transform caveSpawn;

    public static bool loadNext;

    public string scr;
  

    void Awake()
    {
        //Get player
        GameManager.Player = GameObject.FindGameObjectWithTag("Player");
        //Start by spawning in a cave
        currentCave = Instantiate(caves[Random.Range(0, 3)], caveSpawn.position, Quaternion.identity);
        PlayerSpawn();

    }

    public void NextCave() 
    {
        //Destroy old cave
        Destroy(currentCave);
        //Spawn new one
        currentCave = Instantiate(caves[Random.Range(0, 3)], caveSpawn.position, Quaternion.identity);
        PlayerSpawn();


        
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
    }
}
