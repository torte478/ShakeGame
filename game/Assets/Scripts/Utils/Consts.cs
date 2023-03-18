using UnityEngine;

namespace Shake.Utils
{
    internal static class Consts
    {
        public const float Eps = 0.01f;
        public static Vector3 Outside => new(0, -10);
    }
}