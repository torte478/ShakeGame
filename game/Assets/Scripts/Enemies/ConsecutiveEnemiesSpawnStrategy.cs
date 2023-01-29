using System;
using System.Collections.Generic;
using Shake.Area;
using Shake.Enemies.Enemy;
using Shake.Utils;

namespace Shake.Enemies
{
    internal sealed class ConsecutiveEnemiesSpawnStrategy : IEnemiesSpawnStrategy
    {
        private readonly Zones _zones;

        public ConsecutiveEnemiesSpawnStrategy(Zones zones)
        {
            _zones = zones;
        }

        public void Spawn(IReadOnlyCollection<Enemy.Enemy> enemies, Action callback)
        {
            var enemy = GetEnemyToSpawn(enemies);
            enemy.Match(_ => Spawn(_, callback));
        }

        private static Maybe<(Enemy.Enemy enemy, bool isLast)> GetEnemyToSpawn(IReadOnlyCollection<Enemy.Enemy> enemies)
        {
            //TODO: rewrite
            var i = -1;
            
            foreach (var enemy in enemies)
            {
                ++i;
                
                if (enemy.EnemyState == Enemy.Enemy.State.Start)
                    return Maybe.Some((enemy, i == enemies.Count - 1));
                
                if (enemy.EnemyState == Enemy.Enemy.State.Spawn)
                    return Maybe.None<(Enemy.Enemy, bool)>();
            }
            
            return Maybe.None<(Enemy.Enemy, bool)>();
        }
        
        private void Spawn((Enemy.Enemy enemy, bool isLast) _, Action callback)
        {
            var start = _zones.ToPoint(isSpawn: true, zone: Zone.Any);
            var finish = _zones.ToPoint(isSpawn: false, zone: Zone.Any);

            _.enemy.Spawn(
                strategy: new ConsecutiveSpawnStrategy(start, finish),
                callback: _.isLast
                              ? callback
                              : () => { });
        }
    }
}