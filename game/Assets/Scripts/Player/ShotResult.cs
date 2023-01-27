using UnityEngine;
using Utils;

namespace Player
{
    internal struct ShotResult
    {
        public ShotResultType Type { get; }
        public Maybe<Vector3> Point { get; }   

        public ShotResult(ShotResultType type, Vector3 point)
        {
            Type = type;
            Point = Maybe.Some(point);
        }

        public ShotResult(ShotResultType type)
        {
            Type = type;
            Point = Maybe.None<Vector3>();
        }
    }
}