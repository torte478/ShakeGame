using System;
using UnityEngine;

namespace Shake.Creatures.Enemies.Enemy
{
    internal interface IAttack
    {
        event Action Finish;

        void Attack(Vector3 target);

        bool IsAttack(int step);
    }
}