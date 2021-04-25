using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{

    Animator anim;

    AudioSource ratnoise;

    bool del;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();

        ratnoise = transform.GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("running"))
            {
                if (LightSystem.lanternUp)
                {
                    anim.SetTrigger("Play");
                    ratnoise.Play();
                }

                //del = true;
            }

        }
      


    }

    // Update is called once per frame
    void Update()
    {
        if (del) 
        {
            //If animation is complete
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("running"))
            {
                Destroy(gameObject);
            }
        }
    }
}
