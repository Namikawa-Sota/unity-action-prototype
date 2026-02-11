using UnityEngine;

public class PlayerIdleState : IState
{
    private readonly Player _player;

    public PlayerIdleState(Player player)
    {
        this._player = player;
    }

    public void Enter()
    {
        //Debug.Log("Enter Idle");
    }

    public void Exit()
    {
        //Debug.Log("Exit Idle");
    }

    public void Update()
    {
        if (this._player.InputHandler.IsAttacking.Value)
        {
            this._player.StateController.ChangeState(this._player.AttackState);
        }
        else if (this._player.InputHandler.IsAvoid.Value)
        {
            this._player.StateController.ChangeState(this._player.AvoidState);
        }
        else if (this._player.InputHandler.MoveInput != Vector2.zero)
        {
            this._player.StateController.ChangeState(this._player.MoveState);
        }
    }
}
