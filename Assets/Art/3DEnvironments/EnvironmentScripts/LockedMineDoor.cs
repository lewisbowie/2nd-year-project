using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedMineDoor : MonoBehaviour {

    public Collider2D blockingCollider;



    private Animator animator; // Cached variable

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isBroken", true);
            blockingCollider.enabled = false;
        }
    }

    
}
