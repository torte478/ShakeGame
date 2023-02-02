using System;
using System.Collections;
using Shake.Enemies.Bullets;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed class RemoteAttack : IAttack
    {
        private readonly Pool _bullets;
        private readonly float _delay;

        public RemoteAttack(Pool bullets, float delay)
        {
            _bullets = bullets;
            _delay = delay;
        }

        public IEnumerator Start(Transform transform, Action callback)
        {
            yield return new WaitForSeconds(_delay);

            _bullets.Spawn(transform.position);

            callback();
        }
    }
}