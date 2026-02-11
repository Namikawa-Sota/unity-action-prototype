using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private enum CharacterType
    {
        Player,
        Enemy
    }

    [SerializeField] private CharacterType _targetCharaType;
    private BoxCollider _boxCollider;
    private int _damage;
    private float _hitStop;
    private String _targetTag;

    [SerializeField] private List<GameObject> _hitEnemies = new List<GameObject>();

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;

        if (_targetCharaType == CharacterType.Enemy)
        {
            _targetTag = "Enemy";
        }
        else
        {
            _targetTag = "Player";
        }
    }

    public void EnableCollider(AttackParam data)
    {
        _hitEnemies.Clear();
        _damage = data.AttackDamage;
        _hitStop = data.HitStop;
        _boxCollider.enabled = true;
    }

    public void DisableCollider()
    {
        _boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag) && !_hitEnemies.Contains(other.gameObject))
        {
            GameObject enemy = other.gameObject;
            CharacterStatus characterStatus = enemy.GetComponent<CharacterStatus>();

            if (characterStatus != null)
            {
                characterStatus.TakeDamage(_damage);
                HitStopManager.Instance.PlayHitStop(_hitStop);
                _hitEnemies.Add(enemy);
            }
        }
    }
}
