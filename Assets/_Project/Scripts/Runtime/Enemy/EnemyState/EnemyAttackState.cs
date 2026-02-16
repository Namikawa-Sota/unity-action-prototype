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

        Transform targetTransform = _enemy.DetectionAreaCollider.PlayerObjects[0].transform;
        Vector3 targetPos = targetTransform.position;

        _enemy.EnemyAnimationController.StartWalkAnimation();
        _enemy.EnemyMovementController.SetRotationTypeMove();
        _enemy.EnemyMovementController.Move(targetPos);

        await DelayOrArriveAsync(token);

        _enemy.EnemyAnimationController.StopWalkAnimation();
        _enemy.EnemyMovementController.SetRotationTypeTarget(targetTransform);
        _enemy.EnemyAnimationController.SetAttackTrigger();

        try
        {
            await UniTask.Delay(5000, cancellationToken: token);
        }
        catch
        {

        }

        _enemy.EnemyMovementController.SetRotationTypeLock();
        IsFinished = true;
    }

    private async UniTask DelayOrArriveAsync(CancellationToken token)
    {
        try
        {
            //var timeout = UniTask.Delay(5000, cancellationToken: token);

            while (true)
            {
                token.ThrowIfCancellationRequested();

                if (_enemy.EnemyMovementController.HasArrived())
                {
                    _enemy.EnemyAnimationController.StopWalkAnimation();
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
    }
}