using UnityEngine;

public class EnemyDeathState : IEnemyState
{
    private readonly Enemy _enemy;
    public bool IsFinished { get; private set; }

    public EnemyDeathState(Enemy enemy)
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