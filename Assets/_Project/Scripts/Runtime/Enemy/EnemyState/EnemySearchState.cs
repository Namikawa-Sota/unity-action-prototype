using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

public class EnemySearchState : IEnemyState
{
    private readonly Enemy _enemy;
    private CancellationTokenSource _cts;
    public bool IsFinished { get; private set; }

    public EnemySearchState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public void Enter()
    {
        //Debug.Log("Enter Idle");
        _cts = new CancellationTokenSource();
        IsFinished = false;

        var pos = _enemy.EnemyMovementController.GetRandomPosition();
        _enemy.EnemyMovementController.Move(pos);
        _enemy.EnemyMovementController.SetRotationTypeMove();

        DelayOrArriveAsync(_cts.Token).Forget();
    }

    public void Exit()
    {
        //Debug.Log("Exit Idle");
        _enemy.EnemyMovementController.SetRotationTypeLock();

        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        IsFinished = false;
    }

    public void Update()
    {

    }

    private async UniTaskVoid DelayOrArriveAsync(CancellationToken token)
    {
        try
        {
            //var timeout = UniTask.Delay(5000, cancellationToken: token);

            while (true)
            {
                token.ThrowIfCancellationRequested();

                if (_enemy.EnemyMovementController.HasArrived())
                {
                    await UniTask.Delay(1000, cancellationToken: token);
                    break;
                }

                //if (timeout.Status == UniTaskStatus.Succeeded)
                //break;

                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
        catch
        {
            return;
        }

        IsFinished = true;
    }

}