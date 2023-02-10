using Shake.Creatures;
using UnityEngine;

namespace Shake.Enemies
{
    [RequireComponent(typeof(Factory))]
    internal sealed class Enemies : Creatures<Enemy.Enemy>
    {
        private Factory _factory;

        private int _dead;

        [SerializeField]
        private Kind kind;

        void Awake()
        {
            _factory = GetComponent<Factory>();
        }

        protected override void ProcessDeath()
        {
            if (++_dead >= Config.count)
                StartSpawn();
        }

        protected override Enemy.Enemy SpawnCreature()
            => _factory.Create(kind); 

        protected override void PrepareSpawn()
        {
            _dead = 0;
        }
    }
}