using UnityEngine;

public class EnemyStateController
{
    private IEnemyState _currentState;
    public IEnemyState CurrentState => _currentState;
    private readonly Enemy _enemy;
    private readonly EnemyStatus _enemyStatus;
    private UtilityAI _utilityAI;
    private AIContext _context;

    public IEnemyState IdleState { get; private set; }
    public IEnemyState SearchState { get; private set; }
    public IEnemyState DamageState { get; private set; }
    public IEnemyState DeathState { get; private set; }
    public IEnemyState AlertState { get; private set; }
    public IEnemyState AttackState { get; private set; }
    public IEnemyState HeavyAttackState { get; private set; }
    public IEnemyState DashAttackState { get; private set; }
    public IEnemyState GuardState { get; private set; }
    public IEnemyState AvoidState { get; private set; }

    public EnemyStateController(Enemy enemy, EnemyStatus enemyStatus)
    {
        _enemy = enemy;
        _enemyStatus = enemyStatus;

        _utilityAI = new UtilityAI();
        _context = new AIContext();

        IdleState = new EnemyIdleState(enemy);
        SearchState = new EnemySearchState(enemy);
        DamageState = new EnemyDamageState(enemy);
        DeathState = new EnemyDeathState(enemy);
        AlertState = new EnemyAlertState(enemy);
        AttackState = new EnemyAttackState(enemy);
        HeavyAttackState = new EnemyHeavyAttackState(enemy);
        DashAttackState = new EnemyDashAttackState(enemy);
        GuardState = new EnemyGuardState(enemy);
        AvoidState = new EnemyAvoidState(enemy);

        Initialize(IdleState);
    }

    private void Initialize(IEnemyState startState)
    {
        _currentState = startState;
        _currentState.Enter();
    }

    public void ChangeState(IEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Update()
    {
        _currentState?.Update();

        if (_currentState != null && _currentState.IsFinished)
        {
            ContextUpdate(_context);
            TransitionState();
        }
    }

    public void TransitionState()
    {
        ActionType nextAction = _utilityAI.Decide(_context);

        switch (nextAction)
        {
            case ActionType.NormalAttack:
                ChangeState(AttackState);
                break;
            case ActionType.DashAttack:
                ChangeState(DashAttackState);
                break;
            case ActionType.HeavyAttack:
                ChangeState(HeavyAttackState);
                break;
            case ActionType.Guard:
                ChangeState(GuardState);
                break;
            case ActionType.Avoid:
                ChangeState(AvoidState);
                break;
            case ActionType.Search:
                ChangeState(SearchState);
                break;
            case ActionType.None:
                break;
        }
    }

    private void ContextUpdate(AIContext aiContext)
    {
        if (aiContext == null) return;

        // 戦闘状態なら情報を更新する
        if (_enemy.DetectionAreaCollider.PlayerObjects.Count == 0)
        {
            aiContext.EnemyInfo.IsDiscoverd = false;
            return;
        }
        else
        {
            aiContext.EnemyInfo.IsDiscoverd = true;
        }

        // モンスター情報
        aiContext.EnemyInfo.HP = _enemyStatus.Hp;
        aiContext.EnemyInfo.IsStiffening = true;
        aiContext.EnemyInfo.CanAct = true;
        aiContext.EnemyInfo.Position = _enemy.EnemyTransform.position;
        aiContext.EnemyInfo.NormalAttackRange = 2f;
        aiContext.EnemyInfo.Stamina = 10;
        aiContext.EnemyInfo.CurrentState = _enemyStatus.CurrentState;
        aiContext.EnemyInfo.Aggressiveness = _enemyStatus.Aggressiveness;
        aiContext.EnemyInfo.Cautiousness = _enemyStatus.Cautiousness;
        aiContext.EnemyInfo.Cowardliness = _enemyStatus.Cowardliness;
        aiContext.EnemyInfo.Calculativeness = _enemyStatus.Calculativeness;

        // プレイヤー情報
        var player = _enemy.DetectionAreaCollider.PlayerObjects[0];
        aiContext.PlayerInfo.Position = player.transform.position;
        aiContext.PlayerInfo.Distance = Vector3.Distance(_enemy.EnemyTransform.position, player.transform.position);

        PlayerStatus playerStatus = player.GetComponent<PlayerStatus>();
        aiContext.PlayerInfo.HP = playerStatus.Hp;
        aiContext.PlayerInfo.CurrentState = playerStatus.CurrentState;

        // 行動可否情報
        aiContext.Constraints.CanNormalAttack = true;
        aiContext.Constraints.CanHeavyAttack = true;
        aiContext.Constraints.CanGuard = true;
        aiContext.Constraints.CanAvoid = true;
        aiContext.Constraints.CanDashAttack = true;

        // 戦況を更新
        //aiContext.BattleInfo
    }
}
