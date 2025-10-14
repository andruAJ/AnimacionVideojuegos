using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public State currentState;

    public NavMeshAgent agent;
    public Transform player;
    public Transform[] waypoints;
    private int waypointsIndex = 0;

    private void Start()
    {
        ChangeState(new IdleState());
    }
    private void Update()
    {
        currentState?.Update();
    }
    private void ChangeState(State newState) 
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
    private void NextWaypoint() 
    {
        waypointsIndex = (waypointsIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[waypointsIndex].position);
    }
    public bool PlayerInRange(float range) 
    {
        return Vector3.Distance(transform.position, player.position) < range;
    }
}
