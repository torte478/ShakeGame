using System;
using Shake.Utils;
using UnityEngine;

namespace Shake.Player
{
    internal struct ShotResult
    {
        public ShotResultType Type { get; }
        public Maybe<Vector2> Point { get; }
        public Maybe<bool> Left { get; }

        public ShotResult(ShotResultType type, Vector2 point, bool left)
        {
            Type = type;
            Point = Maybe.Some(point);
            Left = Maybe.Some(left);
        }

        public ShotResult(ShotResultType type)
        {
            Type = type;
            Point = Maybe.None<Vector2>();
            Left = Maybe.None<bool>();
        }

        public override string ToString()
            => Type switch
            {
                ShotResultType.Shot => $"Shot: {(Left.Value ? "left" : "right")} {Point.Value}",
                _ => Enum.GetName(typeof(ShotResultType), Type)
            };
    }
}