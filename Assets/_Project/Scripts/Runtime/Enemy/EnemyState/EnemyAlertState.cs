using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyAlertState : IEnemyState
{
    private readonly Enemy _enemy;
    private CancellationTokenSource _cts;

    public bool IsFinished { get; private set; }

    public EnemyAlertState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter Alert");
        _cts = new CancellationTokenSource();

        IsFinished = false;
        Walk(_cts.Token).Forget();
    }

    public void Exit()
    {
        Debug.Log("Exit Alert");
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        IsFinished = false;
    }

    public void Update()
    {

    }

    private async UniTaskVoid Walk(CancellationToken token)
    {
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