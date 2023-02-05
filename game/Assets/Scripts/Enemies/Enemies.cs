using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Shake.Enemies
{
    [RequireComponent(typeof(Factory))]
    internal sealed class Enemies : MonoBehaviour
    {
        private Factory _factory;

        private Vector3 _target;
        
        private int _dead;

        [SerializeField]
        private Area.Area area;

        [SerializeField]
        private Config config;

        void Awake()
        {
            _factory = GetComponent<Factory>();
        }

        public void Init(Vector3 target)
        {
            _target = target;
            StartCoroutine(SpawnEnemies());
        }
        
        public void CheckDamage(Vector3 shot)
        {
            CheckShot(shot);
            if (_dead >= config.count)
                StartSpawn();
        }

        private void StartSpawn()
        {
            _dead = 0;
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            foreach (var _ in Enumerable.Range(0, config.count))
            {
                var enemy = _factory.Create(config.kind);
                
                var path = BuildPath();
                var start = config.spawn switch
                {
                    Spawn.Consecutive => area.ToPoint(isSpawn: true),
                    Spawn.Instant => path[0],
                    
                    _ => throw new Exception($"Unknown type {config.spawn}")
                };
                
                enemy.Init(start, path, _target);

                if (config.spawn == Spawn.Consecutive)
                    yield return new WaitForSeconds(config.spawnDelay);
            }
        }

        private Vector3[] BuildPath()
            => Enumerable
               .Range(0, config.pathLength)
               .Select(_ => area.ToPoint())
               .ToArray();
        
        private void CheckShot(Vector3 shot)
        {
            // TODO: to layer-based collision
            var target = Physics2D.OverlapPoint(shot);
            
            if (target == null)
                return;

            if (!target.gameObject.TryGetComponent<Enemy.Enemy>(out var enemy))
                return;
            
            var killed = enemy.Damage();
            if (killed)
                ++_dead;
        }
    }
}