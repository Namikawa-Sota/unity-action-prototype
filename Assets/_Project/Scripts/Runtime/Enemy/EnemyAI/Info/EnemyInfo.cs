using UnityEngine;

public class EnemyInfo
{
    public float HP;
    public bool IsStiffening;           // 硬直中？
    public bool CanAct;                 // 行動可能？
    public bool IsDiscoverd;            // プレイヤーを発見しているか
    public Vector3 Position;
    public float NormalAttackRange;
    public float Stamina;               // 任意。スタミナ制なら
    public EnemyStateType CurrentState; // 現在の状態
    public float Aggressiveness;      // 攻撃性
    public float Cautiousness;        // 慎重さ
    public float Cowardliness;        // 臆病さ
    public float Calculativeness;     // 賢さ

    public EnemyInfo()
    {
        HP = 0f;
        IsStiffening = false;
        CanAct = true;
        IsDiscoverd = false;
        Position = new Vector3(0, 0, 0);
        NormalAttackRange = 2f;
        CurrentState = EnemyStateType.Idle;
    }
}