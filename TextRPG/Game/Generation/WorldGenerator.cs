using System;

namespace TextRPG.Game.Generation
{
    public class WorldGenerator
    {
        private World World;

        public World Generate(PlayerEntity player)
        {
            World = new World();
            World.Add(player);

            GenerateCities();

            return World;
        }

        private void GenerateCities()
        {
            
        }
    }
}