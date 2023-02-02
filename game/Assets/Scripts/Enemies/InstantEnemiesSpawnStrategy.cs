using System;
using System.Collections.Generic;
using Shake.Area;
using Shake.Enemies.Enemy;

namespace Shake.Enemies
{
    internal sealed class InstantEnemiesSpawnStrategy : IEnemiesSpawnStrategy
    {
        private readonly Area.Area _area;
        
        public InstantEnemiesSpawnStrategy(Area.Area area)
        {
            _area = area;
        }

        public void Spawn(IReadOnlyCollection<Enemy.Enemy> enemies, Action callback)
        {
            foreach (var enemy in enemies)
                enemy.Spawn(new InstantSpawnStrategy(), () => { });

            callback();
        }
    }
}