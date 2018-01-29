namespace TextRPG.Game.Mechanics
{
    public enum CombatActionType
    {
        Attack,
        RetreatSuccess,
        RetreatFail
    }

    public class CombatAction
    {
        public CombatActionType Type;
        public CombatActor Actor, Target;
        public Damage Damage;
    }
}