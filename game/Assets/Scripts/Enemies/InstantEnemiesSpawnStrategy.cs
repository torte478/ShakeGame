using System.Collections.Generic;
using Shake.Area;
using Shake.Enemies.Enemy;

namespace Shake.Enemies
{
    internal sealed class InstantEnemiesSpawnStrategy : IEnemiesSpawnStrategy
    {
        private readonly Zones _zones;
        
        public InstantEnemiesSpawnStrategy(Zones zones)
        {
            _zones = zones;
        }

        public void Spawn(IReadOnlyCollection<Enemy.Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                var position = _zones.ToPoint(isSpawn: false, zone: Zone.Any);
                enemy.Spawn(new InstantSpawnStrategy(position));
            }
        }
    }
}