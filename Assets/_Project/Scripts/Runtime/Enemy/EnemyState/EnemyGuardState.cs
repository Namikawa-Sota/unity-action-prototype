using UnityEngine;

public class EnemyGuardState : IEnemyState
{
    private readonly Enemy _enemy;
    public bool IsFinished { get; private set; }

    public EnemyGuardState(Enemy enemy)
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