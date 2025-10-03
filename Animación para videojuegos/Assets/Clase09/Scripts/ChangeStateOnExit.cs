using UnityEngine;

public class ChangeStateOnExit : StateMachineBehaviour
{
    [SerializeField] private string inputStatName;
    [SerializeField] private string outputStatName;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(outputStatName, animator.GetFloat(inputStatName));
    }
}