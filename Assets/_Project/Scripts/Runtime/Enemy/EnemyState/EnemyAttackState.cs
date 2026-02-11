using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private readonly Enemy _enemy;
    private CancellationTokenSource _cts;
    public bool IsFinished { get; private set; }


    public EnemyAttackState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter Attack");

        _cts = new CancellationTokenSource();
        IsFinished = false;

        Attack(_cts.Token).Forget();
    }

    public void Exit()
    {
        Debug.Log("Exit Attack");

        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        IsFinished = false;
    }

    public void Update()
    {

    }

    private async UniTaskVoid Attack(CancellationToken token)
    {
        if (_enemy.DetectionAreaCollider.PlayerObjects.Count == 0)
        {
            //_enemy.StateController.TransitionState();
            IsFinished = true;
            return;
        }

        Transform player = _enemy.DetectionAreaCollider.PlayerObjects[0].transform;

        float stopDistance = 1.5f; // 衝突しない距離

        // 敵 → プレイヤー の方向
        Vector3 directionToPlayer =
            (player.position - _enemy.transform.position).normalized;

        // プレイヤーの手前で止まる位置
        Vector3 targetPos =
            player.position - directionToPlayer * stopDistance;

        _enemy.EnemyMovementController.Move(targetPos);

        try
        {
            await UniTask.Delay(5000, cancellationToken: token);
        }
        catch
        {

        }

        IsFinished = true;
    }
}