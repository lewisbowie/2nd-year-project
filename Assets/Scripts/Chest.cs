using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    //public GameObject Collectible;
    public float Range;
    public GameObject[] objects;
    public Transform SpawnPoint;
    private bool opened;


   void Start()
    {
       
    }
    void Update()
    {
      
       

            if (Input.GetKeyDown(KeyCode.O) && !opened)
            {
                OpenChest();
            }
       
   
    }

    void OpenChest()
    {
        opened = true;
        GameObject Collectible = Instantiate(objects[Random.Range(0, objects.Length)], SpawnPoint.position, SpawnPoint.rotation) as GameObject;
    }


}
 
