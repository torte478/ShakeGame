using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed partial class Enemy : MonoBehaviour
    {
        public State EnemyState { get; private set; } = State.Start;

        public void Spawn(ISpawnStrategy strategy)
        {
            EnemyState = State.Spawn;
            strategy.Spawn(transform, OnSpawnComplete);
        }

        private void OnSpawnComplete()
        {
            EnemyState = State.Ready;
        }
    }
}