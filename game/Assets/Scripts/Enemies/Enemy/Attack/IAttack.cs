using System;
using UnityEngine;

namespace Shake.Enemies.Enemy.Attack
{
    internal interface IAttack
    {
        event Action Finish;

        void Attack(Vector3 target);

        bool IsAttack(int step);
    }
}