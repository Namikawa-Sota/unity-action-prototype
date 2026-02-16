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
    }

    public void Exit()
    {

    }



    public void Update()
    {

    }
}