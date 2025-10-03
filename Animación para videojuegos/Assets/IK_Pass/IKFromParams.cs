using UnityEngine;

public class IKFromParams : MonoBehaviour
{
    [Header("Targets")]
    public Transform righthandTarget;
    public Transform rightElbowTarget;
    public Transform lefthandTarget;
    public Transform leftElbowTarget;
    public Transform lookTarget;

    [Header("Keys for animation")]
    public string pHRPos = "RH_IK", pHRot = "RH_IKRot", pHHint = "RH_IKHint", pLH ="LH_IK", pLook = "Look_IK";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnAnimatorIK(int layerIndex)
    {
        if (animator || animator.isHuman)
        {
            
            float rightHandIK = animator.GetFloat(pHRPos);
            float rightElbowIK = animator.GetFloat(pHHint);
            float leftHandIK = animator.GetFloat(pLH);
            float leftElbowIK = animator.GetFloat(pLH);
            float lookAtIk = animator.GetFloat(pLook);
            float wHRot = animator.GetFloat(pHRot);

            // Right Hand

            if (righthandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandIK);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, animator.GetFloat(pHRot));
                animator.SetIKPosition(AvatarIKGoal.RightHand, righthandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, righthandTarget.rotation);
            }
            else 
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
            // Right Elbow

            if (rightElbowTarget != null)
            {
                animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rightElbowIK);
                animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowTarget.position);
            }
            else 
            {
                animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
            }
            // Left Hand

            if (lefthandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandIK);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandIK);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, lefthandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, lefthandTarget.rotation);
            }
            else 
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
            // Look At 

            if (lookTarget != null)
            {
                animator.SetLookAtWeight(lookAtIk);
                animator.SetLookAtPosition(lookTarget.position);
            }
            else {animator.SetLookAtWeight(0); }
            // wH Rot

            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, wHRot);
            if (wHRot != null) 
            {
                
            }
        }
    }
}
