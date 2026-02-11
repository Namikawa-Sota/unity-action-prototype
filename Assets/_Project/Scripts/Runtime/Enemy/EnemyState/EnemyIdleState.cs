using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private readonly Enemy _enemy;
    public bool IsFinished { get; private set; }

    public EnemyIdleState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter Idle");
        IsFinished = true;
    }

    public void Exit()
    {
        Debug.Log("Exit Idle");
    }

    public void Update()
    {

    }
}