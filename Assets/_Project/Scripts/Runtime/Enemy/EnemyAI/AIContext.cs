public class AIContext
{
    public EnemyInfo EnemyInfo;
    public PlayerInfo PlayerInfo;
    public BattleInfo BattleInfo;
    public ActionConstraints Constraints;

    public AIContext()
    {
        EnemyInfo = new EnemyInfo();
        PlayerInfo = new PlayerInfo();
        BattleInfo = new BattleInfo();
        Constraints = new ActionConstraints();
    }
}