using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour {

    [Header("Animations")]
    public Animator anim;
    public Animator anim2;
    public Animator anim3;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            anim.Play("LiftMovement2");
            anim2.Play("LeverMovement");
            anim3.Play("CageFall");


        }
    }
}
