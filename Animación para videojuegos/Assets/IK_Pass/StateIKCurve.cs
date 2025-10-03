using UnityEngine;

public class StateIKCurve : StateMachineBehaviour
{
    public AnimationCurve weight = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(layerIndex)) return;
        if (animator)
        {

            float curveValue = animator.GetFloat("IKCurve");
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, curveValue);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, curveValue);
        }
    }
}
