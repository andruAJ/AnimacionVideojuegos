using UnityEngine;
using UnityEngine.Animations;


public class EnableRagdollOnExit : StateMachineBehaviour
{
    private bool fired;
    public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fired = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fired) return;   
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        if (fired)
        {
            return;
        }
        var rc = animator.GetComponent<RagdollController>();
        if (rc != null)
        {
            rc.EnableRagdoll();
            fired = true;
        }
    }
}
