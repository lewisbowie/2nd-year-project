﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {

    [Header("Animations")]
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
     

        if (collision.gameObject.tag == "Player")
        {
            anim.enabled = true;
        }

        if(collision.gameObject.tag == "Player3")
        {
            anim.enabled = true;
        }

    }
}
