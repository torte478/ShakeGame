using System;

namespace Shake.Zones
{
    [Flags]
    internal enum Zone : short
    {
        Left = 1,
        LeftCenter = 2,
        RightCenter = 4,
        Right= 8,
        Center = LeftCenter | RightCenter,
        LeftHalf = Left | LeftCenter,
        RightHalf = RightCenter | Right,
        All = Left | LeftCenter | RightCenter | Right
    }
}