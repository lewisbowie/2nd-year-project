﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour {

    [Header("Animations")]
    public Animator anim;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            anim.Play("LiftAnim2");
            anim2.Play("LeverMovement2");
            anim3.Play("CageFall");
            anim4.Play("BarrierBreak");

        }
    }
}
