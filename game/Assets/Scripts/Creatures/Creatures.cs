using System;
using System.Collections;
using System.Linq;
using Shake.Utils;
using Shake.Zones;
using UnityEngine;

namespace Shake.Creatures
{
    internal abstract class Creatures<T> : MonoBehaviour
        where T : Creature
    {
        private float _depth;

        [SerializeField]
        private LayerMask layer;
        
        [SerializeField]
        private Config config;
        
        [SerializeField]
        private Zones.Zones zones;

        protected Config Config => config;

        void Awake()
        {
            _depth = transform.position.z;
        }
        
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
                    Spawn.Consecutive => zones
                                         .ToPoint(isSpawn: true, zone: Convert(config.zone))
                                         .WithZ(_depth),

                    Spawn.Instant => path[0],

                    _ => throw new Exception($"Unknown type {config.spawn}")
                };
                
                creature.Init(start, path, config.stepDelay);

                if (config.spawn == Spawn.Consecutive)
                    yield return new WaitForSeconds(config.spawnDelay);
            }
        }
        
        private Vector3[] BuildPath()
            => Enumerable
               .Range(0, config.pathLength)
               .Select(_ => zones.ToPoint(zone: Convert(config.zone)).WithZ(_depth))
               .ToArray();

        private static Zone Convert(Config.Zone zone)
            => zone switch
            {
                Config.Zone.Left => Zone.LeftHalf,
                Config.Zone.Right => Zone.RightHalf,
                Config.Zone.All => Zone.All,

                _ => throw new Exception("Can't convert Zone: {zone}")
            };
    }
}