namespace TextRPG.Game
{
    public interface IDamageable
    {
        bool IsAlive { get; }
        void Kill();
        void ReceiveDamage(ref Damage damage);
    }
}