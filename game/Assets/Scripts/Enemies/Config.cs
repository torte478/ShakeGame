using System;
using UnityEngine;

namespace Shake.Enemies
{
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
    }
}