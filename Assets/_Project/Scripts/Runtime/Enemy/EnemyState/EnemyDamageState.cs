using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyDamageState : IEnemyState
{
    private readonly Enemy _enemy;
    private CancellationTokenSource _cts;
    public bool IsFinished { get; private set; }

    public EnemyDamageState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter Damage");

        // すでに硬直中ならキャンセル
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        // 硬直処理
        IsFinished = false;
        StiffRoutine(_cts.Token).Forget();
    }

    public void Exit()
    {
        Debug.Log("Exit Damage");
        IsFinished = false;
    }

    public void Update()
    {
        IsFinished = false;
    }

    private async UniTaskVoid StiffRoutine(CancellationToken token)
    {
        try
        {
            // 5秒停止
            await UniTask.Delay(5000, cancellationToken: token);
        }
        catch
        {
            // キャンセル時は無視
        }

        IsFinished = true;
    }
}