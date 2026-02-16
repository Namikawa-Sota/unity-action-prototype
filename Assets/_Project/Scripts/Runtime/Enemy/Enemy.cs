using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Collider detectionCollider;
    [SerializeField] private AttackCollider AttackCollider;

    public EnemyStateController StateController { get; private set; }
    public EnemyMovementController EnemyMovementController { get; private set; }
    public EnemyAnimationController EnemyAnimationController { get; private set; }
    public DetectionAreaCollider DetectionAreaCollider { get; private set; }
    public EnemyStatus EnemyStatus { get; private set; }
    public Transform EnemyTransform { get; private set; }

    private void Awake()
    {
        EnemyMovementController = GetComponent<EnemyMovementController>();
        EnemyAnimationController = GetComponent<EnemyAnimationController>();
        DetectionAreaCollider = detectionCollider.GetComponent<DetectionAreaCollider>();
        EnemyStatus = GetComponent<EnemyStatus>();
        EnemyTransform = transform;

        StateController = new EnemyStateController(this, EnemyStatus);
    }

    private void Start()
    {
        EnemyStatus.OnTakeDamage += OnEnterDamageState;
    }

    private void Update()
    {
        StateController.Update();
    }

    private void OnEnterDamageState()
    {
        this.StateController.ChangeState(StateController.DamageState);
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
}
