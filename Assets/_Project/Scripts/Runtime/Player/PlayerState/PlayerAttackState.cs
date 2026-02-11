using System.Threading;
using Cysharp.Threading.Tasks;


public class PlayerAttackState : IState
{
    private readonly Player _player;
    private CancellationTokenSource _cts;

    public PlayerAttackState(Player player)
    {
        this._player = player;
    }

    public void Enter()
    {
        //Debug.Log("Enter Attack");

        // PlayerCombatControllerがコンボを完了したら、このステートから出るように設定
        _player.PlayerCombatController.OnComboFinished += OnComboCompleted;

        _cts = new CancellationTokenSource();
        _player.PlayerCombatController.StartCombo(_cts.Token).Forget();
    }

    public void Exit()
    {
        //Debug.Log("Exit Attack");
        // ステートを抜けるときに、イベントの購読を解除する
        _player.PlayerCombatController.OnComboFinished -= OnComboCompleted;

        if (_cts != null)
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
            _cts.Dispose();
            _cts = null;
        }
    }

    public void Update()
    {

    }

    private void OnComboCompleted()
    {
        _player.StateController.ChangeState(_player.IdleState);
    }
}
