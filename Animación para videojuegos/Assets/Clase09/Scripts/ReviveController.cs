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
        if (Input.GetKeyDown(reviveKey) && Game.Instance.PlayerOne._currentHealth <= 0)
        {
            StartCoroutine(ReviveCoroutine());
        }
    }

    private IEnumerator ReviveCoroutine()
    {

        animator.SetTrigger("Revive");
        ragdollController.DisableRagdollAndRevive();
        yield return new WaitForSeconds(2f);
    }
}
