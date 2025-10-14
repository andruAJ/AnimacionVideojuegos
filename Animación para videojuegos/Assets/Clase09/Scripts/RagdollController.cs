using System.Linq;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
     
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider rootCollider;
    [SerializeField] Transform hips;
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;

    private bool isRagdoll = false;

    private void Awake() 
    {
        //ragdollRigidbodies = hips.GetComponentsInChildren<Rigidbody>();
        //ragdollColliders = hips.GetComponentsInChildren<Collider>();
        if (animator == null) 
        {
            animator = GetComponent<Animator>();
        }
        if (rb == null) 
        {
            rb = GetComponent<Rigidbody>();
        }

        var allRigidbodies: Rigidbody[] = hips.GetComponentsInChildren<Rigidbody>(includeInactive: true);
        ragdollRigidbodies = allRigidbodies.Where(r => r != rb).ToArray();
        ragdollColliders = hips.GetComponentsInChildren<Collider>(includeInactive: true).Where(c => c != rootCollider).ToArray();

        SetAnimateState(true);
    }

    private void SetAnimateState(bool state) 
    {
        isRagdoll = state;
        animator.enabled = state;
        foreach (var r in ragdollRigidbodies) 
        {
            r.isKinematic = state;
            r.detectCollisions = !state;
            r.linearVelocity = Vector3.zero;
            r.angularVelocity = Vector3.zero;
        }
        foreach (var c in ragdollColliders) 
        {
            c.enabled = !state;
            rootCollider.enabled = state;
            rb.isKinematic = !state;
            animator.enabled = state;
        }
    }

    public void EnableRagdoll() 
    {
        if (isRagdoll) return;
        isRagdoll = true;
        animator.enabled = false;
        rootCollider.enabled = false;
        if (rb != null) 
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        foreach (var r in ragdollRigidbodies) 
        {
            r.isKinematic = false;
            r.detectCollisions = true;
        }
        foreach (var c in ragdollColliders) 
        {
            c.enabled = true;
        }
        Physics.SyncTransforms();
    }

    public void DisableRagdoll() 
    {
        if (!isRagdoll) return;
        SetAnimateState(true);
        if (hips != null)
        {
            transform.position = hips.position;
        }
    }



}
