using UnityEngine;
using UniRx;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public BoolReactiveProperty IsAttacking { get; } = new();
    public BoolReactiveProperty IsAvoid { get; } = new();

    private PlayerControls _controls;

    private void Awake()
    {
        this._controls = new PlayerControls();

        this._controls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        this._controls.Player.Move.canceled += _ => MoveInput = Vector2.zero;

        this._controls.Player.Attack.performed += _ => IsAttacking.Value = true;
        this._controls.Player.Attack.canceled += _ => IsAttacking.Value = false;

        this._controls.Player.Avoid.performed += _ => IsAvoid.Value = true;
        this._controls.Player.Avoid.canceled += _ => IsAvoid.Value = false;
    }

    private void OnEnable() => this._controls?.Enable();
    private void OnDisable() => this._controls?.Disable();
}
