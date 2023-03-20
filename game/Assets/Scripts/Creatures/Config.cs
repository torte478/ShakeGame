using System;
using Shake.Utils;
using UnityEngine;

namespace Shake.Creatures
{
    [Serializable]
    internal sealed class Config
    {
        public Spawn spawn;

        public Zone zone;

        [Min(1)]
        public int count;

        [Min(1)]
        public int pathLength;

        [Min(Consts.Eps)]
        public float spawnDelay;

        [Min(0f)]
        public float stepDelay;

        public enum Zone
        {
            Left,
            Right,
            All
        }
    }
}