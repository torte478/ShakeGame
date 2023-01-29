using System.Collections.Generic;

namespace Shake.Enemies
{
    // TODO : rename to single word
    internal interface IEnemiesSpawnStrategy
    {
        void Spawn(IReadOnlyCollection<Enemy> enemies);
    }
}