using System;
using System.Collections;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed class RemoteAttack : IAttack
    {
        private readonly Bullets.Bullets _bullets;
        private readonly float _delay;
        private readonly Vector3 _playerPosition;

        public RemoteAttack(Bullets.Bullets bullets, float delay, Vector3 playerPosition)
        {
            _bullets = bullets;
            _delay = delay;
            _playerPosition = playerPosition;
        }

        public IEnumerator Start(Transform transform, Action callback)
        {
            yield return new WaitForSeconds(_delay);

            _bullets.Shot(transform.position, _playerPosition);

            callback();
        }
    }
}