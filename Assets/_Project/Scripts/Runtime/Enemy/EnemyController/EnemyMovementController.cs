using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _radius;

    private NavMeshAgent _agent;
    private NavMeshHit _hit;
    private EnemyRotationType _rotationType = EnemyRotationType.Locked;
    private Transform _targetTransform;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (_rotationType)
        {
            case EnemyRotationType.Movement:
                RotationMoveDirection();
                break;

            case EnemyRotationType.Target:
                RotationTargetDirection();
                break;

            case EnemyRotationType.Locked:
                break;
        }
    }

    public void Move(Vector3 targetPos)
    {
        _agent.SetDestination(targetPos);
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 randomPos = _enemy.EnemyTransform.position +
                            new Vector3(Random.Range(-_radius, _radius), 0f, Random.Range(-_radius, _radius));

        if (NavMesh.SamplePosition(randomPos, out _hit, 2f, NavMesh.AllAreas))
        {
            return _hit.position;
        }

        return _enemy.EnemyTransform.position;
    }

    public bool HasArrived()
    {
        if (!_agent.enabled)
            return false;

        if (!_agent.isOnNavMesh)
            return false;

        if (_agent.pathPending)
            return false;

        if (_agent.remainingDistance > _agent.stoppingDistance)
            return false;

        // agentが停止しているか（微妙な誤差対策）
        if (_agent.hasPath && _agent.velocity.sqrMagnitude > 0f)
            return false;

        return true;
    }

    public void Stop()
    {
        if (_agent.hasPath)
        {
            _agent.ResetPath();
        }
    }

    public void SetRotationTypeMove()
    {
        _rotationType = EnemyRotationType.Movement;
    }

    public void SetRotationTypeTarget(Transform target)
    {
        _targetTransform = target;
        _rotationType = EnemyRotationType.Target;
    }

    public void SetRotationTypeLock()
    {
        _rotationType = EnemyRotationType.Locked;
    }

    private void RotationMoveDirection()
    {
        // NavMeshAgent が無い or 移動していない場合は何もしない
        if (_agent == null) return;

        Vector3 velocity = _agent.velocity;
        velocity.y = 0f;

        // ほぼ停止中なら回転しない
        if (velocity.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
        _enemy.EnemyTransform.rotation = Quaternion.Slerp(_enemy.EnemyTransform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    private void RotationTargetDirection()
    {
        {
            if (_targetTransform == null) return;

            Vector3 dir = _targetTransform.position - _enemy.EnemyTransform.position;
            dir.y = 0f;

            if (dir.sqrMagnitude < 0.001f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(dir.normalized);
            _enemy.EnemyTransform.rotation = Quaternion.Slerp(_enemy.EnemyTransform.rotation, targetRotation, 10f * Time.deltaTime);
        }

    }
}