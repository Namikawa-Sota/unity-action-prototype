using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    //_enemy.EnemyAnimator.SetBool("IsMoving", true);

    private Animator _enemyAnimator;

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {

    }
}