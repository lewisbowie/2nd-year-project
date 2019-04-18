using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FlyTextAppears : MonoBehaviour {
    public GameObject Fly;
    private bool flyactive;
	// Use this for initialization
	void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
       // flyactive = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (flyactive)
       // {
            if (collision.gameObject.tag == "Player")
            {
                Fly.gameObject.SetActive(true);

            }
        }
   // }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!flyactive)
        {
            if (collision.gameObject.tag == "Player")
            {
                Fly.gameObject.SetActive(false);

            }
        }
    }
}
