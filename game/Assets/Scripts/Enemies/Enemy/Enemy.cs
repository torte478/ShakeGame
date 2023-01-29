using System;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed partial class Enemy : MonoBehaviour
    {
        private int _hp;

        public State EnemyState { get; private set; } = State.Start;
        public EnemyConfig EnemyConfig { get; set; }

        public void Spawn(ISpawnStrategy strategy, Action callback)
        {
            _hp = EnemyConfig.Hp;
            
            EnemyState = State.Spawn;
            strategy.Spawn(transform, EnemyConfig.Speed, OnSpawnComplete);
        }

        private void OnSpawnComplete()
        {
            EnemyState = State.Ready;
        }

        public bool Hurt()
        {
            --_hp;
            if (_hp > 0)
                return false;

            EnemyState = State.Dead;
            transform.position = EnemyConfig.Spawn;
            return true;
        }
    }
}