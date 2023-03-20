﻿using UnityEngine;

namespace Shake.Utils
{
    internal static class Vector2Extensions
    {
        public static Vector3 WithZ(this Vector2 origin, float z = 0f)
            => new Vector3(origin.x, origin.y, z);
    }
}