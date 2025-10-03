using UnityEngine;

public class HitReceiver : MonoBehaviour, Ihittable
{
    [SerializeField] private Animator animator;
    [SerializeField] private string hitTrigger = "Hit";
    public void ApplyHit(HitInfo hitInfo)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }
        // Additional logic for handling the hit can be added here
        Debug.Log($"Hit received at {hitInfo.HitPoint} with damage {hitInfo.Damage}");
    }
    private void Reset()
        {
            animator = GetComponentInChildren<Animator>();
        }
}
