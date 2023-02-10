using UnityEngine;

namespace Shake.Creatures.Enemies
{
    internal sealed class Enemies : Creatures<Enemy.Enemy>
    {
        private int _dead;

        [SerializeField]
        private Kind kind;

        [SerializeField]
        private Factory factory;

        protected override void ProcessDeath()
        {
            if (++_dead >= Config.count)
                StartSpawn();
        }

        protected override Enemy.Enemy SpawnCreature()
            => factory.Create(kind); 

        protected override void PrepareSpawn()
        {
            _dead = 0;
        }
    }
}