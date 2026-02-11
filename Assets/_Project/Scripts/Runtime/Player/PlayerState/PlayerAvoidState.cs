using UnityEngine;

public class PlayerAvoidState : IState
{
    private readonly Player _player;

    public PlayerAvoidState(Player player)
    {
        this._player = player;
    }

    public void Enter()
    {
        _player.PlayerAnimator.SetTrigger("Avoid");
        _player.PlayerMovementController.OnAvoidFinished += OnAvoidCompleted;
    }

    public void Exit()
    {
        _player.PlayerMovementController.OnAvoidFinished -= OnAvoidCompleted;
    }

    public void Update()
    {

    }

    private void OnAvoidCompleted()
    {
        _player.StateController.ChangeState(_player.IdleState);
    }
}