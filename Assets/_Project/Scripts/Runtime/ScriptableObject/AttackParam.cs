using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/CreateAttackParam")]
public class AttackParam : ScriptableObject
{
    [SerializeField] private String _attackName;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackHitStop;
    [SerializeField] private float _damageHitStop;

    public int AttackDamage => _attackDamage;
    public float AttackHitStop => _attackHitStop;
    public float DamageHitStop => _damageHitStop;
}
