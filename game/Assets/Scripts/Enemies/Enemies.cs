using System.Linq;
using Shake.Area;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies
{
    internal sealed class Enemies : MonoBehaviour
    {
        private Enemy[] _enemies;

        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private Zones zones;
        
        [SerializeField, Min(0f)]
        private int count;
        
        [SerializeField, Min(0f)]
        private float speed;

        void Start()
        {
            _enemies = Enumerable
                       .Range(0, count)
                       .Select(
                           _ => Instantiate(prefab, zones.Spawn, Quaternion.identity, transform)
                               .GetComponent<Enemy>())
                       .ToArray();
        }
        
        public void DoSpawn()
        {
            var enemy = GetEnemyToSpawn();
            if (enemy.IsNone)
                return;

            Spawn(enemy.Value);
        }

        private void Spawn(Enemy enemy)
        {
            var start = zones.ToPoint(isSpawn: true, zone: Zone.Any);
            var finish = zones.ToPoint(isSpawn: false, zone: Zone.Any);

            enemy.Spawn(start, finish, speed);
        }

        private Maybe<Enemy> GetEnemyToSpawn()
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.State == State.Start)
                    return Maybe.Some(enemy);
                
                if (enemy.State == State.Spawn)
                    return Maybe.None<Enemy>();
            }
            
            return Maybe.None<Enemy>();
        }
    }
}