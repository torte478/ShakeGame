using System.Collections.Generic;
using UnityEngine;

namespace Shake.Creatures
{
    internal interface ICreature
    {
        bool Damage();
        void Init(Vector3 position, IReadOnlyCollection<Vector3> path);
    }
}