using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    public EnemyStateType CurrentState { get; private set; }

    public float Aggressiveness => _aggressiveness;
    public float Cautiousness => _cautiousness;
    public float Cowardliness => _cowardliness;
    public float Calculativeness => _calculativeness;

    // 攻撃性
    [SerializeField][Range(0f, 1f)] private float _aggressiveness;
    // 慎重さ
    [SerializeField][Range(0f, 1f)] private float _cautiousness;
    // 臆病さ
    [SerializeField][Range(0f, 1f)] private float _cowardliness;
    // 賢さ
    [SerializeField][Range(0f, 1f)] private float _calculativeness;

    private void Start()
    {
        Initialize();
        CurrentState = EnemyStateType.Idle;
    }
}
