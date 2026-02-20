using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/CreatePlayerAttackCombo")]
public class PlayerAttackComboConfig : ScriptableObject
{
    [SerializeField] private AttackParam[] _attackComboSteps;

    [SerializeField] private String _animatorIntName;

    public AttackParam[] ComboSteps => _attackComboSteps;
    public String AnimatorIntName => _animatorIntName;
}
