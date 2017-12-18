namespace TextRPG.Game
{
    public class WorldEntity : Entity
    {
        public World World { get; set; }
        
        public WorldEntity(World world)
        {
            World = world;
        }
    }
}