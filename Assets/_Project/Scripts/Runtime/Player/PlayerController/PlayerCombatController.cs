// PlayerCombatController.cs
using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UniRx;
using System;
using System.Threading;

public class PlayerCombatController : MonoBehaviour
{
    // Playerクラスへの参照はインスペクターで設定
    [SerializeField] private Player _player;
    [SerializeField] private PlayerAttackComboConfig _attackComboConfig;

    private int _currentComboIndex = 0;

    private UniTaskCompletionSource _comboInputStartCompletionSource;
    private UniTaskCompletionSource _comboInputEndCompletionSource;
    private UniTaskCompletionSource _animationEndCompletionSource;

    // コンボが完了したことを通知するイベント
    public Action OnComboFinished;



    // アニメーションイベント：コンボ入力受付開始
    public void OnReadyForNextCombo()
    {
        _comboInputStartCompletionSource?.TrySetResult();
    }

    // アニメーションイベント: コンボ入力受付終了
    public void OnStopComboInput()
    {
        _comboInputEndCompletionSource?.TrySetResult();
    }

    public void OnFinishAnimation()
    {
        _animationEndCompletionSource?.TrySetResult();
    }

    public async UniTask StartCombo(CancellationToken token)
    {
        _currentComboIndex = 0;

        try
        {
            while (_currentComboIndex < _attackComboConfig.ComboSteps.Length)
            {
                // トークンでキャンセルされたら例外になる場合があるので、毎回チェック
                token.ThrowIfCancellationRequested();

                _comboInputStartCompletionSource = new UniTaskCompletionSource();
                _comboInputEndCompletionSource = new UniTaskCompletionSource();
                _animationEndCompletionSource = new UniTaskCompletionSource();

                var currentAttackParam = _attackComboConfig.ComboSteps[_currentComboIndex];

                _player.PlayerAnimator.SetInteger(_attackComboConfig.AnimatorIntName, _currentComboIndex + 1);

                // 入力受付開始待ち
                await _comboInputStartCompletionSource.Task.AttachExternalCancellation(token);

                // 入力受付処理
                var inputTask = _player.InputHandler.IsAttacking
                    .Where(x => x)
                    .Take(1)
                    .ToTask(token)
                    .AsUniTask();

                // WhenAny をトークンで守る
                var result = await UniTask.WhenAny(
                    inputTask,
                    _comboInputEndCompletionSource.Task.AttachExternalCancellation(token)
                );

                if (result.Item1) // コンボ受付成功
                {
                    await _comboInputEndCompletionSource.Task.AttachExternalCancellation(token);
                    _currentComboIndex++;
                }
                else
                {
                    break;
                }
            }

            _player.PlayerAnimator.SetInteger(_attackComboConfig.AnimatorIntName, 0);

            await _animationEndCompletionSource.Task.AttachExternalCancellation(token);

            OnComboFinished?.Invoke();
        }
        catch (OperationCanceledException)
        {
            // キャンセル時の終了処理（モーション戻しなど）
            _player.PlayerAnimator.SetInteger(_attackComboConfig.AnimatorIntName, 0);
            // イベントは Exit() 側で解除されるのでここでは不要
        }
    }
}