using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TextRPG.Game.Mechanics
{
    public class Combat
    {
        private List<CombatActor> Actors;
        public bool IsFinished
        {
            get;
            private set;
        }

        private CombatInitiativeStatsVisitor InitiativeStatsVisitor;

        public Combat(IEnumerable<CombatActor> actors)
        {
            Actors = actors.ToList();
            InitiativeStatsVisitor = new CombatInitiativeStatsVisitor();
        }

        public void Tick()
        {
            var sortedByInitiative = Actors
                .OrderByDescending(actor => InitiativeStatsVisitor.Calculate(actor));

            foreach(var actor in sortedByInitiative)
            {
                // TODO fight
            }

        }

    }
}