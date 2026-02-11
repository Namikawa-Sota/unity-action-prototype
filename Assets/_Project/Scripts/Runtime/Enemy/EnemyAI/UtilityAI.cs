using UnityEngine;

public class UtilityAI
{
    public ActionType Decide(AIContext ctx)
    {
        if (ctx.EnemyInfo.IsDiscoverd == false)
        {
            return ActionType.Search;
        }

        float normal = EvaluateNormalAttack(ctx);
        float dash = EvaluateDashAttack(ctx);
        float heavy = EvaluateHeavyAttack(ctx);
        float guard = EvaluateGuard(ctx);
        float avoid = EvaluateAvoid(ctx);

        float max = normal;
        ActionType action = ActionType.NormalAttack;

        if (dash > max) { max = dash; action = ActionType.DashAttack; }
        if (heavy > max) { max = heavy; action = ActionType.HeavyAttack; }
        if (guard > max) { max = guard; action = ActionType.Guard; }
        if (avoid > max) { max = avoid; action = ActionType.Avoid; }

        return max > 0f ? action : ActionType.None;
    }

    private float EvaluateNormalAttack(AIContext ctx)
    {
        if (!ctx.Constraints.CanNormalAttack)
        {
            return 0f;
        }

        float distanceScore = Mathf.Clamp01(
            1f - (ctx.PlayerInfo.Distance / ctx.EnemyInfo.NormalAttackRange)
        );

        float baseScore = 0f;
        baseScore += distanceScore * 0.5f;
        baseScore += ctx.EnemyInfo.CanAct ? 0.3f : 0f;
        baseScore -= ctx.PlayerInfo.CurrentState == PlayerStateType.Guard ? 0.2f : 0f;

        // TODO 戦闘が長引くと下がり続ける
        //      評価対象となる行動に範囲を設けるべきかも
        baseScore -= ctx.BattleInfo.PlayerAvoidCount * 0.05f; // 癖読み

        // 性格バイアス
        baseScore += ctx.EnemyInfo.Cautiousness * 0.3f;
        baseScore -= ctx.EnemyInfo.Aggressiveness * 0.2f;

        // ノイズ
        baseScore *= Random.Range(0.9f, 1.1f);

        return Mathf.Max(0f, baseScore);
    }

    private float EvaluateHeavyAttack(AIContext ctx)
    {
        return 0f;
    }

    private float EvaluateDashAttack(AIContext ctx)
    {
        return 0f;
    }

    private float EvaluateGuard(AIContext ctx)
    {
        return 0f;
    }

    private float EvaluateAvoid(AIContext ctx)
    {
        return 0f;
    }
}