public class PlayerStatus : CharacterStatus
{
    public PlayerStateType CurrentState { get; private set; }

    private void Start()
    {
        Initialize();
        CurrentState = PlayerStateType.Idle;
    }
}
