using System.Collections.Generic;
using Shake.Area;

namespace Shake.Enemies
{
    internal sealed class InstantEnemiesSpawnStrategy : IEnemiesSpawnStrategy
    {
        private readonly Zones _zones;
        
        public InstantEnemiesSpawnStrategy(Zones zones)
        {
            _zones = zones;
        }

        public void Spawn(IReadOnlyCollection<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                var position = _zones.ToPoint(isSpawn: false, zone: Zone.Any);
                enemy.Spawn(new InstantSpawnStrategy(position));
            }
        }
    }
}