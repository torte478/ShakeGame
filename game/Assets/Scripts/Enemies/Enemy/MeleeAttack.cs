using System;
using System.Collections;
using DG.Tweening;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed class MeleeAttack : IAttack
    {
        private readonly Vector3 _target;
        private readonly float _speed;

        public MeleeAttack(Vector3 target, float speed)
        {
            _target = target;
            _speed = speed;
        }

        public IEnumerator Start(Transform transform, Action callback)
        {
            transform
                .DOTimingMove(_target, _speed)
                .SetEase(Ease.Linear);

            yield break;
        }
    }
}