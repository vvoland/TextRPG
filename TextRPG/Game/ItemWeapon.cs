using System;

namespace TextRPG.Game
{
    public class ItemWeapon : Item
    {
        public int Strength
        {
            get;
            protected set;
        }
        public float Speed
        {
            get;
            protected set;
        }

        public ItemWeapon(string name) : base(name, DefaultPlural(name))
        {
            
        }

        private static string DefaultPlural(string name)
        {
            return name + "s";
        }

        public ItemWeapon(string name, string plural) : base(name, plural)
        {
        }

        public ItemWeapon SetStrength(int str)
        {
            Strength = str;
            return this;
        }

        public override bool CanUse(IUsageContext context)
        {
            return context.CanUse(this);
        }

        public override void Use(IUsageContext context)
        {
            context.Use(this);
        }

        public ItemWeapon SetSpeed(float speed)
        {
            Speed = speed;
            return this;
        }
    }

}