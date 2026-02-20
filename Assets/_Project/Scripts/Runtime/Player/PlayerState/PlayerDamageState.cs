using UnityEngine;

public class PlayerDamageState : IState
{
    private readonly Player _player;

    public PlayerDamageState(Player player)
    {
        this._player = player;
    }

    public void Enter()
    {
        Debug.Log("Enter Damage State");

        // ダメージアニメーション
        // 終了まで待つ
        // 遷移
        _player.StateController.ChangeState(_player.IdleState);
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }
}