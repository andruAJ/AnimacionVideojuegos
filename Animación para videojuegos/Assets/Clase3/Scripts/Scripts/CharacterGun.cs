using System;
using GA.Sessions.Class_03.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterGun : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private bool automatic = true;
    [SerializeField] private bool requireAim = false;
    [SerializeField] private float fireRate = 10f;

    [SerializeField] private float range = 200f;

    [SerializeField] private float debugDuration = 0.2f;

    [SerializeField] private Transform traceOrigins;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Animator animator;

    [SerializeField] private Camera mainCamera;
    private bool isFiring;
    public float nextShootTime;

    //Header
    [SerializeField] float camShakeRecoil = 0.6f;
    [SerializeField] float cameraKick = 0.12f;
    [SerializeField] float cameraRecover = 0.2f;

    private RecoilCameraKick recoilCameraKick;

    public Character ParentCharacter { get; set; }

    void Start()
    {
        recoilCameraKick = mainCamera.GetComponent<RecoilCameraKick>();
    }
    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.started) isFiring = true;
        if (ctx.canceled) isFiring = false;
        if (!automatic && ctx.performed) TryShoot();
    }
    public void TryShoot()
    {
        if (requireAim && (ParentCharacter == null || !ParentCharacter.IsAiming)) return;
        if (Time.time < nextShootTime) return;
        nextShootTime = Time.time + 1f / Mathf.Max(1f, fireRate);
        ShootOnce();

    }
    public void Update()
    {
        if (isFiring && automatic)
        {
            TryShoot();
        }
    }
    private void ShootOnce()
    {
        if (animator != null)
        {
            animator.SetTrigger("Fire"); 
        }
        if (recoilCameraKick) recoilCameraKick.Kick(camShakeRecoil, cameraKick, cameraRecover);
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 from = traceOrigins != null ? traceOrigins.position : ray.origin;
        if (Physics.Raycast(ray, out var hit, range, enemyMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 to = hit.point;
            Debug.DrawRay(start: ray.origin, dir: ray.direction * Vector3.Distance(ray.origin, to), color: Color.red, duration: debugDuration);
            Debug.DrawLine(from, to, Color.yellow, debugDuration);

            var info = new HitInfo
            {
                HitPoint = hit.point,
                HitNormal = hit.normal,
                Damage = 10f // Example damage value, adjust as needed
            };
            if (hit.collider.TryGetComponent<Ihittable>(out var ihittable))
            {
                ihittable.ApplyHit(info);
            }
            else
            {
                Debug.LogWarning("Hit object does not implement Ihittable interface.");
            }
        }
        else
        {
            Vector3 to = ray.origin + ray.direction * range;
            Debug.DrawRay(start: ray.origin, dir: ray.direction * range, color: Color.grey, duration: debugDuration);
            Debug.DrawLine(from, to, Color.cyan, debugDuration);
        }
    }
}
