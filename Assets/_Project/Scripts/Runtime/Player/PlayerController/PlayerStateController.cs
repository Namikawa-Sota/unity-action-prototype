public class PlayerStateController
{
    private IState _currentState;
    public IState CurrentState => this._currentState;

    public void Initialize(IState startState)
    {
        this._currentState = startState;
        this._currentState.Enter();
    }

    public void ChangeState(IState newState)
    {
        this._currentState?.Exit();
        this._currentState = newState;
        this._currentState?.Enter();
    }

    public void Update()
    {
        this._currentState?.Update();
    }
}
