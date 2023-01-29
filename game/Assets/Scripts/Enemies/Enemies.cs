using System;
using System.Linq;
using Shake.Area;
using UnityEngine;

namespace Shake.Enemies
{
    internal sealed partial class Enemies : MonoBehaviour
    {
        private Enemy.Enemy[] _enemies;
        private State _state = State.Spawn;
        private IEnemiesSpawnStrategy _spawn;

        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private Zones zones;

        [SerializeField, Min(0f)]
        private int count;
        
        [SerializeField, Min(0f)]
        private float speed;
        
        [SerializeField]
        private SpawnType spawnType;

        void Start()
        {
            _enemies = Enumerable
                       .Range(0, count)
                       .Select(
                           _ => Instantiate(prefab, zones.Spawn, Quaternion.identity, transform)
                               .GetComponent<Enemy.Enemy>())
                       .ToArray();

            _spawn = spawnType switch
            {
                SpawnType.Consecutive => new ConsecutiveEnemiesSpawnStrategy(zones, speed),
                SpawnType.Instant => new InstantEnemiesSpawnStrategy(zones),
                _ => throw new Exception(spawnType.ToString())
            };
        }
        
        public void DoSpawn()
        {
            if (_state != State.Spawn)
                return;
            
            if (IsSpawnComplete())
                _state = State.Ready;
            else
                _spawn.Spawn(_enemies);
        }

        // TODO: to callback?
        private bool IsSpawnComplete()
            => _enemies.All(_ => _.EnemyState != Enemy.Enemy.State.Start 
                                 && _.EnemyState != Enemy.Enemy.State.Spawn);
    }
}