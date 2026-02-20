using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 50f;
    [SerializeField] private float _gravity = -9.81f;


    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckRadius = 0.3f;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private AttackCollider AttackCollider;

    private bool _isGrounded;
    private Vector3 _velocity;

    public float RunSpeed => this._runSpeed;
    public float RotationSpeed => this._rotationSpeed;

    public Transform CameraTransform => this._cameraTransform;

    public PlayerStatus PlayerStatus { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public CharacterController CharaController { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    public PlayerStateController StateController { get; private set; }
    public PlayerMovementController PlayerMovementController { get; private set; }
    public PlayerCombatController PlayerCombatController { get; private set; }

    public IState IdleState { get; private set; }
    public IState MoveState { get; private set; }
    public IState AttackState { get; private set; }
    public IState AvoidState { get; private set; }
    public IState DamageState { get; private set; }
    public IState DeathState { get; private set; }

    private void Awake()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        CharaController = GetComponent<CharacterController>();
        PlayerAnimator = GetComponent<Animator>();
        StateController = new PlayerStateController();
        PlayerMovementController = GetComponent<PlayerMovementController>();
        PlayerCombatController = GetComponent<PlayerCombatController>();
        PlayerStatus = GetComponent<PlayerStatus>();

        this.IdleState = new PlayerIdleState(this);
        this.MoveState = new PlayerMoveState(this);
        this.AttackState = new PlayerAttackState(this);
        this.AvoidState = new PlayerAvoidState(this);
        this.DamageState = new PlayerDamageState(this);
        this.DeathState = new PlayerDeathState(this);
    }

    private void Start()
    {
        StateController.Initialize(this.IdleState);
        PlayerStatus.OnTakeDamage += OnEnterDamageState;
    }

    private void Update()
    {
        StateController.Update();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);

        if (_isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * Time.fixedDeltaTime;

        CharaController.Move(_velocity * Time.fixedDeltaTime);
    }

    // アニメーションイベントから呼び出される仲介役の関数
    public void EnableAttackCollider(AttackParam data)
    {
        // AttackColliderスクリプトのEnableColliderメソッドを呼び出す
        AttackCollider.EnableCollider(data);
    }

    // 攻撃を終了する際の仲介役
    public void DisableAttackCollider()
    {
        AttackCollider.DisableCollider();
    }

    // ダメージを受けた時の状態遷移
    private void OnEnterDamageState()
    {
        this.StateController.ChangeState(DamageState);
    }
}
