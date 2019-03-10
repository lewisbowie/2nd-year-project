using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3 : MonoBehaviour {

    [Header("Animations")]
    public Animator anim;
    public Animator anim2;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            anim.Play("LiftMovement3");
            anim2.Play("LeverMovement");


        }
    }
}
