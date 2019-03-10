using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button5 : MonoBehaviour {

    [Header("Animations")]
    public Animator anim;
    public Animator anim2;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            anim.Play("LiftMovement4");
            anim2.Play("LeverMovement");


        }
    }
}
