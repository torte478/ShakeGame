using UnityEngine;

namespace Player
{
    internal struct ShotResult
    {
        public ShotResultType Type { get; }
        public Vector2 Point { get; } // TODO: to Maybe   

        public ShotResult(
            ShotResultType type,
            Vector2 point = default)
        {
            Type = type;
            Point = point;
        }
    }
}