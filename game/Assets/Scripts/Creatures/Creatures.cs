using System;
using System.Collections;
using System.Linq;
using Shake.Enemies;
using UnityEngine;

namespace Shake.Creatures
{
    internal abstract class Creatures<T> : MonoBehaviour
        where T : ICreature
    {
        [SerializeField]
        private LayerMask layer;
        
        [SerializeField]
        private Config config;
        
        [SerializeField]
        private Area.Area area;

        protected Config Config => config;
        
        void Start()
        {
            Player.Player.Instance.Shot += CheckDamage;

            StartSpawn();
        }

        protected abstract void ProcessDeath();

        protected abstract T SpawnCreature();

        protected virtual void PrepareSpawn()
        {
        }
        
        protected void StartSpawn()
        {
            PrepareSpawn();
            StartCoroutine(SpawnCreatures());
        }
        
        private void CheckDamage(Vector3 shot)
        {
            var isDeath = CheckShot(shot);
            if (isDeath)
                ProcessDeath();
        }
        
        private bool CheckShot(Vector3 shot)
        {
            var target = Physics2D.OverlapPoint(shot, layer);
            if (target == null)
                return false;

            if (!target.gameObject.TryGetComponent<T>(out var creature))
                return false;
            
            var killed = creature.Damage();
            return killed;
        }

        private IEnumerator SpawnCreatures()
        {
            foreach (var _ in Enumerable.Range(0, config.count))
            {
                var creature = SpawnCreature();
                
                var path = BuildPath();
                var start = config.spawn switch
                {
                    Spawn.Consecutive => area.ToPoint(isSpawn: true, region: config.region),
                    Spawn.Instant => path[0],
                    
                    _ => throw new Exception($"Unknown type {config.spawn}")
                };
                
                creature.Init(start, path);

                if (config.spawn == Spawn.Consecutive)
                    yield return new WaitForSeconds(config.spawnDelay);
            }
        }
        
        private Vector3[] BuildPath()
            => Enumerable
               .Range(0, config.pathLength)
               .Select(_ => area.ToPoint(region: config.region))
               .ToArray();
    }
}