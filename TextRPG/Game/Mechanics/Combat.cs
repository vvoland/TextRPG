using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TextRPG.Game.Mechanics
{
    public class Combat
    {
        public Action<CombatAction> OnAction;

        public PlayerEntity Player
        {
            get;
            private set;
        }

        public CombatActor PlayerActor
        {
            get;
            private set;
        }

        public IEnumerable<CombatActor> Actors
        {
            get
            {
                return _Actors;
            }
        }

        public IEnumerable<CombatActor> Enemies
        {
            get
            {
                return Actors.Where(a => a != PlayerActor);
            }
        }

        public bool IsFinished
        {
            get;
            private set;
        }

        public bool FinishedByRetreat
        {
            get;
            private set;
        }

        public bool PlayerDead
        {
            get;
            private set;
        }

        public bool EnemiesDead
        {
            get;
            private set;
        }

        private List<CombatActor> _Actors;
        private CombatInitiativeStatsVisitor InitiativeStatsVisitor;

        public Combat(PlayerEntity player, IEnumerable<CombatActor> enemies)
        {
            Player = player;
            _Actors = enemies.ToList();
            PlayerActor = new CombatActor(player, player.Stats, player.Damageable, player);
            _Actors.Add(PlayerActor);
            InitiativeStatsVisitor = new CombatInitiativeStatsVisitor();
            SetTargets();
        }

        private void SetTargets()
        {
            foreach(var enemy in Enemies)
            {
                enemy.Target = PlayerActor;
            }
            PlayerActor.Target = Enemies.FirstOrDefault();
        }

        public void Tick()
        {
            if(IsFinished)
                return;

            var sortedByInitiative = Actors
                .OrderByDescending(actor => InitiativeStatsVisitor.Calculate(actor.Stats));

            foreach(var actor in sortedByInitiative)
            {
                if(actor.AttemptRetreat)
                {
                    actor.AttemptRetreat = false;
                    if(AttemptRetreat(actor))
                        break;
                }
                else if(actor.IsAlive)
                {
                    Attack(actor);
                }
            }

            EnemiesDead = Enemies.All(a => !a.IsAlive);
            PlayerDead = !Player.IsAlive && !EnemiesDead;
            IsFinished = PlayerDead || EnemiesDead || FinishedByRetreat;
        }

        private bool AttemptRetreat(CombatActor actor)
        {
            int initiative = InitiativeStatsVisitor.Calculate(actor.Stats);
            int chance = 20 + initiative;
            var rng = new Random();
            FinishedByRetreat = rng.Next(100) < chance;
            OnAction(new CombatAction
            {
                Actor = actor,
                Target = actor,
                Damage = null,
                Type = FinishedByRetreat ? CombatActionType.RetreatSuccess : CombatActionType.RetreatFail
            });

            return FinishedByRetreat;
        }

        private void Attack(CombatActor actor)
        {
            var dmg = actor.DamageDealer.Damage();
            if (actor.Target != null)
            {
                actor.Target.Damageable.ReceiveDamage(ref dmg);
                OnAction(new CombatAction
                {
                    Type = CombatActionType.Attack,
                    Actor = actor,
                    Target = actor.Target,
                    Damage = dmg
                });
            }
        }

    }
}