using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    static public void SaveScore() 
    {
        //Convert our score to JSON
        string convertedData = JsonUtility.ToJson(FindObjectOfType<GameManager>());
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + "score", convertedData);
    }

    static public int ReadScore() 
    {

        GameManager loadedManager = new GameManager();
        try 
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/score"), loadedManager);
            return loadedManager.savedScore;

        }
        catch 
        {
            return 0;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
