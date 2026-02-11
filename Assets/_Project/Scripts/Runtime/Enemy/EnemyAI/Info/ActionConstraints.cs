public class ActionConstraints
{
    public bool CanNormalAttack;
    public bool CanHeavyAttack;
    public bool CanGuard;
    public bool CanAvoid;
    public bool CanDashAttack;

    public ActionConstraints()
    {
        CanNormalAttack = true;
        CanHeavyAttack = true;
        CanGuard = true;
        CanAvoid = true;
        CanDashAttack = true;
    }
}
