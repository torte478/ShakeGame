using Shake.Utils;
using UnityEngine;

namespace Shake.Player
{
    internal sealed class Shot
    {
        public Maybe<Vector2> Point { get; }
        public Maybe<bool> Left { get; }

        private Shot(Vector2 point, bool left)
        {
            Point = Maybe.Some(point);
            Left = Maybe.Some(left);
        }
        
        private Shot()
        {
            Point = Maybe.None<Vector2>();
            Left = Maybe.None<bool>();
        }

        public static Shot Misfire() => new();
        public static Shot Create(Vector2 point, bool left) => new(point, left);

        public override string ToString()
            => Point.To(
                _ => $"Shot: {(Left.Value ? "left" : "right")} {Point.Value}",
                () => "Misfire");
    }
}