using System;
using System.Collections.Generic;
using System.Linq;
using Shake.Area;
using Shake.Enemies.Bullets;
using Shake.Enemies.Enemy;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies
{
    internal sealed partial class Enemies : MonoBehaviour
    {
        private Enemy.Enemy[] _enemies;
        private State _state = State.Spawn;
        private IEnemiesSpawnStrategy _spawn;
        private int _dead;

        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private Zones zones;

        [SerializeField]
        private Config config;

        [SerializeField]
        private Pool bullets;

        void Start()
        {
            //TODO: rename
            var enemyConfig = new EnemyConfig(
                hp: config.hp,
                speed: config.speed,
                spawn: zones.Spawn,
                attack: config.attackStep,
                attackDelay: config.remoteAttackDelay);
            
            _enemies = Enumerable
                       .Range(0, config.count)
                       .Select(_ => InstantiateEnemy(enemyConfig))
                       .ToArray();

            _spawn = config.spawnType switch
            {
                SpawnType.Consecutive => new ConsecutiveEnemiesSpawnStrategy(zones),
                SpawnType.Instant => new InstantEnemiesSpawnStrategy(zones),
                _ => throw new Exception(config.spawnType.ToString())
            };
        }
        
        public void Process(Maybe<Vector2> shot)
        {
            shot.To(CheckShot);
            Spawn();
        }

        private void Spawn()
        {
            if (_dead == _enemies.Length)
            {
                _dead = 0;
                _state = State.Spawn;
            }
            
            if (_state != State.Spawn)
                return;
            
            _spawn.Spawn(_enemies, () => { _state = State.Ready; });
        }

        private Enemy.Enemy InstantiateEnemy(EnemyConfig enemyConfig)
        {
            var enemy = Instantiate(prefab, zones.Spawn, Quaternion.identity, transform)
                        .GetComponent<Enemy.Enemy>();

            enemy.Init(enemyConfig, bullets, BuildCyclicPath(config.pathLength).ToArray());
            return enemy;
        }

        private IEnumerable<Vector3> BuildCyclicPath(int length)
        {
            var start = zones.ToPoint();
            yield return start;
            
            foreach (var point in Enumerable.Range(0, length).Select(_ => zones.ToPoint()))
                yield return point;

            yield return start;
        }
        
        private void CheckShot(Vector2 shot)
        {
            // TODO: to layer-based collision
            var target = Physics2D.OverlapPoint(shot);
            
            // TODO: fix that shit
            if (target == null)
                return;
            
            var enemy = target.gameObject.GetComponent<Enemy.Enemy>();
            if (enemy == null)
                return;
            
            var killed = enemy.Hurt();
            if (killed)
                ++_dead;
        }
    }
}