using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public State currentState;

    public NavMeshAgent agent;
    public Transform player;
    public Transform[] waypoints;
    private int waypointsIndex = 0;
    public Animator animator;

    [Header("Movement setiings")] public float walkSpeed = 2f;
    public float runSpeed = 3.5f;
    public float rotationSmooth = 12f;
    public float aniimationSmooth = 10f;

    static class Hash
    {
        public static readonly int speedX = Animator.StringToHash("SpeedX");
        public static readonly int speedY = Animator.StringToHash("SpeedY");
    }

    private void Start()
    {
        ChangeState(new IdleState(enemy: this));

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = true;
        animator.applyRootMotion = false;
    }
    private void Update()
    {
        currentState?.Update();

        Vector3 desired = agent.desiredVelocity;
        desired.y = 0;

        if (desired.sqrMagnitude < 0.01f)
        {
            Quaternion look = Quaternion.LookRotation(desired, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * rotationSmooth);
        }

        Vector3 dirLocal = desired.sqrMagnitude > 0.01f ? transform.InverseTransformDirection(desired.normalized) : Vector3.zero;

        float denom = Mathf.Max(0.01f, agent.speed);
        float mag = Mathf.Clamp01(agent.velocity.magnitude/denom);

        float targetX = dirLocal.x * mag;
        float targetY = dirLocal.z * mag;

        float curX = Mathf.Lerp(animator.GetFloat(Hash.speedX), targetX, Time.deltaTime * aniimationSmooth);
        float curY = Mathf.Lerp(animator.GetFloat(Hash.speedY), targetY, Time.deltaTime * aniimationSmooth);
        animator.SetFloat(Hash.speedX, curX);
        animator.SetFloat(Hash.speedY, curY);
    }
    public void ChangeState(State newState) 
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
    public void NextWaypoint() 
    {
        if (waypoints.Length == 0) return;
        waypointsIndex = (waypointsIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[waypointsIndex].position);
    }
    public bool PlayerInRange(float range) 
    {
        return Vector3.Distance(transform.position, player.position) < range;
    }
}
