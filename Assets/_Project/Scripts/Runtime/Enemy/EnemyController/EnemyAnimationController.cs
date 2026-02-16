using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _enemyAnimator;

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {

    }

    public void StartWalkAnimation()
    {
        _enemyAnimator.SetBool("IsMoving", true);
    }

    public void StopWalkAnimation()
    {
        _enemyAnimator.SetBool("IsMoving", false);
    }

    public void SetAttackTrigger()
    {
        _enemyAnimator.SetTrigger("Attack");
    }
}