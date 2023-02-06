using System;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies
{
    [Serializable]
    internal sealed class Config
    {
        public Spawn spawn;
        
        public Kind kind;
        
        [Min(1)]
        public int count;

        [Min(1)]
        public int pathLength;

        [Min(Consts.Eps)]
        public float spawnDelay;
    }
}