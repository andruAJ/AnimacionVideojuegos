using UnityEngine;

public class AnimatorTesterTrigger : MonoBehaviour
{
    public Animator animator;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("fire");
        }
    }
}
