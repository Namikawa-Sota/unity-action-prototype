public class BattleInfo
{
    public float TimeInCurrentState;       // 現在の状態が何秒続いているか
    public ActionType LastPlayerAction;   // プレイヤーが最後にやった行動
    public ActionType LastSelfAction;     // 自分が最後にやった行動
    public int PlayerGuardCount;           // ガード回数（癖）
    public int PlayerAvoidCount;           // 回避回数（癖）

    public BattleInfo()
    {
        TimeInCurrentState = 0f;
        LastPlayerAction = ActionType.None;
        LastSelfAction = ActionType.None;
        PlayerGuardCount = 0;
        PlayerAvoidCount = 0;
    }
}