using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/CreateAttackParam")]
public class AttackParam : ScriptableObject
{
    [SerializeField] private String _attackName;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _hitStop;

    public int AttackDamage => _attackDamage;
    public float HitStop => _hitStop;
}
