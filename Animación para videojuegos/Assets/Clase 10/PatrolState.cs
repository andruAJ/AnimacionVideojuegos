using UnityEngine;

public class PatrolState : State
{
    public PatrolState(EnemyAI enemy) : base(enemy)
    {
       
    }
    public override void Enter()
    {
        enemy.agent.isStopped = false;
        enemy.Next
    }
    public override void Update()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.player.position) < 5f) 
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
    }
    public override void Exit()
    {
    }
}
