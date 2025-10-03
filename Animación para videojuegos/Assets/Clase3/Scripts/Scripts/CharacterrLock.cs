using GA.Sessions.Class_03.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterLock : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private Camera camera;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float detectionAngle;
    [SerializeField] private LayerMask detectionMask;

    public void OnLook(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return ;
        if (ParentCharacter.LockTarget != null)
        {
            ParentCharacter.LockTarget = null;
            return;
        }
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);
        if (detectedObjects.Length == 0) return;
        float nearestAngle = detectionAngle;
        float nearestDistance = detectionRadius;

        int closestObjects = 0;

        Vector3 cameraForward = camera.transform.forward;

        for (int i = 0; i < detectedObjects.Length; i++) 
        {
            Collider obj = detectedObjects[i];
            Vector3 objViewDirection = obj.transform.position - camera.transform.position;
            //float dot
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
#endif
    public Character ParentCharacter { get; set; }
}
