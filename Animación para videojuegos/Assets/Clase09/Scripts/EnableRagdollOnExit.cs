using UnityEngine;
using UnityEngine.Animations;


public class EnableRagdollOnExit : StateMachineBehaviour
{
    [Range(0.8f, 1f)] public float normalizedTime = 0.9f;
    private bool fired;
    public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fired = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fired) return;   
        var rc = animator.GetComponent<RagdollController>();
        if (rc != null)
        {
            rc.EnableRagdoll();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        if (fired)
        {
            return;
        }
        if (stateInfo.normalizedTime >= normalizedTime)
        {
            fired = true;
            var rcon = animator.GetComponent<RagdollController>();
            if (rcon != null)
            {
                rcon.EnableRagdoll();
            }

        }
        var rc = animator.GetComponent<RagdollController>();
        if (rc != null)
        {
            rc.EnableRagdoll();
            fired = true;
        }
    }
}
