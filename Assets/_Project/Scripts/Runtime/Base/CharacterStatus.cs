using System;
using UnityEngine;

/// <summary>
/// キャラクターのステータスを管理
/// </summary>
public class CharacterStatus : MonoBehaviour
{
    // 各種ステータス（攻撃力、防御力、移動速度、最大HP、最大空腹値、空腹の減少量）
    [SerializeField] protected float _attack;
    [SerializeField] protected float _defense;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _maxHunger;
    [SerializeField] protected float _hungerDecreasePerFrame;

    // 各ステータスのゲッター
    public float Attack => _attack;
    public float Defense => _defense;
    public float Speed => _speed;
    public float MaxHp => _maxHp;
    public float MaxHunger => _maxHunger;
    public float HungerDecreasePerFrame => _hungerDecreasePerFrame;

    // 現在のHPと空腹値
    public float Hp { get; private set; }
    public float Hunger { get; private set; }

    //ダメージイベント
    public Action OnTakeDamage;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected void Initialize()
    {
        Hp = _maxHp;
        Hunger = _maxHunger;
    }

    private void Update()
    {
        ReduceHunger();
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    public void TakeDamage(float damage)
    {
        Hp = Mathf.Clamp(Hp - damage, 0, MaxHp);
        OnTakeDamage?.Invoke();
    }

    /// <summary>
    /// 空腹値を減少させる処理
    /// </summary>
    public void ReduceHunger()
    {
        Hunger = Mathf.Clamp(Hunger - HungerDecreasePerFrame, 0f, MaxHunger);
    }
}