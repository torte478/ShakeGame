using System;
using System.Collections.Generic;
using System.Linq;
using Shake.Area;
using Shake.Enemies.Bullets;
using Shake.Enemies.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

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

        [FormerlySerializedAs("zones")]
        [SerializeField]
        private Area.Area area;

        [SerializeField]
        private Config config;

        [SerializeField]
        private Pool bullets;

        [SerializeField]
        private Player.Player player;

        void Start()
        {
            //TODO: rename
            var enemyConfig = new EnemyConfig(
                hp: config.hp,
                speed: config.speed,
                spawn: area.Spawn,
                attack: config.attackStep,
                attackDelay: config.remoteAttackDelay,
                attackSpeed: config.meleeAttackSpeed,
                attackType: config.attackType,
                target: config.target.position);
            
            _enemies = Enumerable
                       .Range(0, config.count)
                       .Select(_ => InstantiateEnemy(enemyConfig))
                       .ToArray();

            _spawn = config.spawnType switch
            {
                SpawnType.Consecutive => new ConsecutiveEnemiesSpawnStrategy(area),
                SpawnType.Instant => new InstantEnemiesSpawnStrategy(area),
                _ => throw new Exception(config.spawnType.ToString())
            };
            
            player.Shot += CheckDamage;

            Spawn();
        }

        void OnDisable()
        {
            player.Shot -= CheckDamage;
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
            var enemy = Instantiate(prefab, area.Spawn, Quaternion.identity, transform)
                        .GetComponent<Enemy.Enemy>();

            enemy.Init(enemyConfig, bullets, BuildCyclicPath(config.pathLength).ToArray());
            return enemy;
        }

        private IEnumerable<Vector3> BuildCyclicPath(int length)
        {
            var start = area.ToPoint();
            yield return start;
            
            foreach (var point in Enumerable.Range(0, length).Select(_ => area.ToPoint()))
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

        private void CheckDamage(Vector2 shot)
        {
            CheckShot(shot);
            Spawn();
        }
    }
}