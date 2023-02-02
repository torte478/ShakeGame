using System;
using System.Collections;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal interface IAttack
    {
        IEnumerator Start(Transform transform, Action callback);
    }
}