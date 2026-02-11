using UnityEngine;

public class PlayerMoveState : IState
{
    private readonly Player _player;

    public PlayerMoveState(Player player)
    {
        this._player = player;
    }

    public void Enter()
    {
        //Debug.Log("Enter Move");
        this._player.PlayerAnimator.SetBool("IsMoving", true);
    }

    public void Exit()
    {
        //Debug.Log("Exit Move");
        this._player.PlayerAnimator.SetBool("IsMoving", false);
    }



    public void Update()
    {
        if (this._player.InputHandler.MoveInput == Vector2.zero)
        {
            this._player.StateController.ChangeState(this._player.IdleState);
        }
        else if (this._player.InputHandler.IsAvoid.Value)
        {
            this._player.StateController.ChangeState(this._player.AvoidState);
        }
        else
        {
            this._player.PlayerMovementController.MoveInCameraSpace(this._player.InputHandler.MoveInput);
        }
    }
}
