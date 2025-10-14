using UnityEngine;

public abstract class State
{
    protected EnemyAI enemy;

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
