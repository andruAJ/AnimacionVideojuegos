using UnityEngine;
using System.Collections;

public class ReviveController : MonoBehaviour
{
    [SerializeField] private KeyCode reviveKey = KeyCode.R;

    private Animator animator;
    private RagdollController ragdollController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ragdollController = GetComponent<RagdollController>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(reviveKey) && Game.Instance.PlayerOne.CurrentHealth <= 0)
        //{
        //    StartCoroutine(ReviveCoroutine());
        //}
    }

    private IEnumerator ReviveCoroutine()
    {
        // Disable ragdoll
        ragdollController.SetAnimateState(true);
        // Play revive animation
        animator.SetTrigger("Revive");
        // Wait for the animation to finish (assuming 2 seconds here, adjust as needed)
        yield return new WaitForSeconds(2f);
        // Restore player health
        //Game.Instance.PlayerOne.RestoreHealth(50); // Restore 50 health points, adjust as needed
    }
}
