using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private float lightCost = 1.5f;
    [SerializeField] private float heavyCost = 3.5f;
    private Animator animator;
    private AttackHitboxController hitboxController;
    private void Awake() 
    {
        animator = GetComponent<Animator>();
        hitboxController = GetComponent<AttackHitboxController>();
    }
    public void OnLightAttack(InputAction.CallbackContext ctx) 
    {
        Debug.Log("Light Attack");
        if (ctx.performed)
            if (Game.Instance.PlayerOne.CurrentStamine > 0) {
                Game.Instance.PlayerOne.DepletStamina(lightCost);
                animator.SetTrigger("Attack");
            }
        
    }
    public void OnHeavyAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Heavy Attack");
        if (ctx.performed || ctx.canceled) 
        {
            Game.Instance.PlayerOne.DepletStamina(heavyCost);
            Debug.Log("Heavy Attack Performed");
            animator.SetTrigger("HeavyAttack");
        }        
        //if (Game.Instance.PlayerOne.CurrentStamine > 0)
        //{


        //}

    }


    public void RotatePlayer(InputAction.CallbackContext ctx) {
        if (ctx.performed || ctx.canceled) {
            Vector3 rotationVector = new(0, 0, 0) {
                y = ctx.ReadValue<float>()
            };

            float rotationSpeed = 500f;
            character.eulerAngles += rotationVector * (rotationSpeed * Time.deltaTime);
        }
        
    }

    private void DepleteStamina(float value) 
    {
        Game.Instance.PlayerOne.DepletStamina(value);
    }

    public void DepleteStaminaWithParameters(string parametro) 
    {
        float motionValue = GetComponent<Animator>().GetFloat(parametro);
        DepleteStamina(motionValue);
    }

    public void ToggleAttackHitBox (int hitBoxID) 
    {
        hitboxController.ToggleHitBoxes();
    }
    public void CleanUpHitbox() 
    {
        hitboxController.CleanUpHitboxes();
    }
}
