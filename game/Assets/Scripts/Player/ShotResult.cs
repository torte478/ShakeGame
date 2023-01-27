using UnityEngine;

namespace Player
{
    internal struct ShotResult
    {
        public ShotResultType Type { get; }
        public Vector3 Point { get; } // TODO: to Maybe   

        public ShotResult(
            ShotResultType type,
            Vector3 point = default)
        {
            Type = type;
            Point = point;
        }
    }
}