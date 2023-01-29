using System.Collections.Generic;
using Shake.Area;
using Shake.Utils;

namespace Shake.Enemies
{
    internal sealed class ConsecutiveEnemiesSpawnStrategy : IEnemiesSpawnStrategy
    {
        private readonly Zones _zones;
        private readonly float _speed; //TODO : to enemy config

        public ConsecutiveEnemiesSpawnStrategy(Zones zones, float speed)
        {
            _zones = zones;
            _speed = speed;
        }

        public void Spawn(IReadOnlyCollection<Enemy> enemies)
        {
            var enemy = GetEnemyToSpawn(enemies);
            if (enemy.IsNone)
                return;

            Spawn(enemy.Value);
        }

        private static Maybe<Enemy> GetEnemyToSpawn(IReadOnlyCollection<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.EnemyState == Enemy.State.Start)
                    return Maybe.Some(enemy);
                
                if (enemy.EnemyState == Enemy.State.Spawn)
                    return Maybe.None<Enemy>();
            }
            
            return Maybe.None<Enemy>();
        }
        
        private void Spawn(Enemy enemy)
        {
            var start = _zones.ToPoint(isSpawn: true, zone: Zone.Any);
            var finish = _zones.ToPoint(isSpawn: false, zone: Zone.Any);

            enemy.Spawn(
                new ConsecutiveSpawnStrategy(start, finish, _speed));
        }
    }
}