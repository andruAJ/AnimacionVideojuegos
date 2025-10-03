using UnityEngine;
using UnityEngine.Animations;

public class CleanUpFrames : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        animator.gameObject.SendMessage("IFrameEnd");
    }

}
