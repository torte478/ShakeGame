﻿using System;
using Shake.Area;
using Shake.Utils;
using UnityEngine;

namespace Shake.Creatures
{
    [Serializable]
    internal sealed class Config
    {
        public Spawn spawn;

        public Region region;

        [Min(1)]
        public int count;

        [Min(1)]
        public int pathLength;

        [Min(Consts.Eps)]
        public float spawnDelay;
    }
}