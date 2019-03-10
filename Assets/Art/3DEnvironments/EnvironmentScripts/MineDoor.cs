using UnityEngine;

public class MineDoor : MonoBehaviour
{
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
            animator.SetBool("isOpen", true);
            blockingCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isOpen", false);
            blockingCollider.enabled = true;
        }
    }
}
