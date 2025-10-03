using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace GA.Sessions.Class_07.Scripts
{
    public class WallAvoidDriver : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private Transform gunProbe;
        [SerializeField] private bool useGunForward;
        [SerializeField] private LayerMask geometryMask;
        [SerializeField] private Rig weaponPoseRig;
        [SerializeField] private MultiPositionConstraint posC;

        [Header("Camera debug references")]
        public float radius = 0.20f;
        public float maxCheck = 1f;
        public float smooth = 0.08f;

        [Header("Histeresis")]
        public float enterStart = 0.5f;
        public float enterFull = 0.35f;
        public float exitStart = 0.7f;
        public float exitFull = 0.45f;
        private bool avoiding;



        private Ray camRayDbg, gunRayDbg;
        private bool camHit, gunHit;
        private Vector3 camPt, camN, gunPt, gunN;
        private float aCam, aGun;
        private float alpha, vel;

        private void Awake()
        {
            if (!cam) cam = Camera.main;
        }

        private void Update()
        {
            if (!cam) return;

            camRayDbg = new Ray(cam.transform.position, cam.transform.forward);

            Vector3 gOrigin = gunProbe ? gunProbe.position : cam.transform.position;
            Vector3 gDirection = (useGunForward && gunProbe) ? gunProbe.forward : cam.transform.forward;
            gunRayDbg = new Ray(gOrigin, gDirection);

            RaycastHit hCam, hGun;
            camHit = Physics.SphereCast(camRayDbg, radius, out hCam, maxCheck, geometryMask, QueryTriggerInteraction.Ignore);
            gunHit = Physics.SphereCast(gunRayDbg, radius, out hGun, maxCheck, geometryMask, QueryTriggerInteraction.Ignore);

            if (camHit)
            {
                camPt = hCam.point;
                camN = hCam.normal;
            }
            else
            {
                camPt = camN = default;
            }
            if (gunHit)
            {
                gunPt = hGun.point;
                gunN = hGun.normal;
            }
            else
            {
                gunPt = gunN = default;
            }

            float distCam = camHit ? hCam.distance : float.PositiveInfinity;
            float distGun = gunHit ? hGun.distance : float.PositiveInfinity;
            float minDist = Mathf.Min(distCam, distGun);

            //Histeresis
            if (!avoiding && (minDist <= enterFull)) avoiding = true;
            if (avoiding && (!camHit && !gunHit || minDist >= exitStart)) avoiding = false;

            float aStart = avoiding ? exitStart : enterStart;
            float aFull = avoiding ? exitFull : enterFull;

            //Alpha por camara/arma con umbrales
            aCam = camHit ? Mathf.Clamp01(Mathf.InverseLerp(aStart, aFull, distCam)) : 0f;
            aGun = gunHit ? Mathf.Clamp01(Mathf.InverseLerp(aStart, aFull, distGun)) : 0f;

            float target = Mathf.Max(aCam, aGun);

            alpha = Mathf.SmoothDamp(alpha, target, ref vel, smooth);

            float k = 1f - alpha;
            if (weaponPoseRig) weaponPoseRig.weight = k;
            if (posC) posC.weight = k;
        }

        private void OnDrawGizmos()
        {
            var c = cam ? cam : Camera.main;
            if (!c) return;

            DrawCapsule(Application.isPlaying ? camRayDbg : new Ray(c.transform.position, c.transform.forward),
                maxCheck, radius, new Color(1, 1, 1, 0.7f));

            if (Application.isPlaying && camHit)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(camPt, radius * 0.6f);
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(camPt, camPt + camN * 0.35f);
            }

            Vector3 gOrigin = gunProbe ? gunProbe.position : c.transform.position;
            Vector3 gDirection = (useGunForward && gunProbe) ? gunProbe.forward : c.transform.forward;

            DrawCapsule(Application.isPlaying ? gunRayDbg : new Ray(c.transform.position, c.transform.forward),
                maxCheck, radius, new Color(1, 0, 1, 0.7f));
            if (Application.isPlaying && gunHit)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(camPt, radius * 0.6f);
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(camPt, camPt + camN * 0.35f);
            }
        }

        void DrawCapsule(Ray ray, float len, float r, Color c)
        {
            Gizmos.color = c;
            Gizmos.DrawWireSphere(ray.origin, r);
            Gizmos.DrawWireSphere(ray.origin + ray.direction * len, r);
            Vector3 up = Vector3.up * r, right = Vector3.right * r;

            Gizmos.DrawLine(ray.origin + up, ray.origin + ray.direction * len + up);
            Gizmos.DrawLine(ray.origin - up, ray.origin + ray.direction * len - up);
            Gizmos.DrawLine(ray.origin + right, ray.origin + ray.direction * len + right);
            Gizmos.DrawLine(ray.origin - right, ray.origin + ray.direction * len - right);


        }
    }
}