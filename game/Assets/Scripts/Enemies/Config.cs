using System;
using Shake.Enemies.Enemy;
using UnityEngine;

namespace Shake.Enemies
{
    // TODO: refactor
    [Serializable]
    internal sealed class Config
    {
        [Min(0f)]
        public int count;
        
        public SpawnType spawnType;

        [Min(0)]
        public int hp;
        
        [Min(0f)]
        public float speed;

        [Min(1)]
        public int pathLength;

        [Min(0)]
        public int attackStep;

        [Min(0f)]
        public float remoteAttackDelay;

        [Min(0f)]
        public float meleeAttackSpeed;

        public Transform target;
    }
}