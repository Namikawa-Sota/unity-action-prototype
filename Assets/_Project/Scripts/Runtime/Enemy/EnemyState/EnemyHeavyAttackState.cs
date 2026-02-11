using UnityEngine;

public class EnemyHeavyAttackState : IEnemyState
{
    private readonly Enemy _enemy;
    public bool IsFinished { get; private set; }

    public EnemyHeavyAttackState(Enemy enemy)
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