using UnityEngine;

public class EnemyDashAttackState : IEnemyState
{
    private readonly Enemy _enemy;
    public bool IsFinished { get; private set; }

    public EnemyDashAttackState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public void Enter()
    {
        IsFinished = false;
    }

    public void Exit()
    {
        IsFinished = false;
    }

    public void Update()
    {

    }
}