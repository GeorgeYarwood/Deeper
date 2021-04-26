using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadScene() 
    {
        SceneManager.LoadScene(1);
    }

    public void HardDiff() 
    {
        LightSystem lightsys = new LightSystem();
        lightsys.drainrate = 25;
        string convertedData = JsonUtility.ToJson(lightsys);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + "lightsys", convertedData);
        SceneManager.LoadScene(1);



    }

    public void EasyDiff()
    {
        LightSystem lightsys = new LightSystem();
        lightsys.drainrate = 10;
        string convertedData = JsonUtility.ToJson(lightsys);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + "lightsys", convertedData);
        SceneManager.LoadScene(1);

    }

    public void ExplorerDiff()
    {
        LightSystem lightsys = new LightSystem();
        lightsys.drainrate = 0;
        string convertedData = JsonUtility.ToJson(lightsys);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + "lightsys", convertedData);
        SceneManager.LoadScene(1);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
