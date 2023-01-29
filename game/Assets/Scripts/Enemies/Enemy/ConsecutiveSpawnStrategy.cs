using DG.Tweening;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed class ConsecutiveSpawnStrategy : ISpawnStrategy
    {
        private readonly Vector3 _from;
        private readonly Vector3 _to;

        public ConsecutiveSpawnStrategy(Vector3 from, Vector3 to)
        {
            _from = from;
            _to = to;
        }

        public void Spawn(Transform transform, float speed, TweenCallback callback)
        {
            transform.position = _from;
            var duration = Vector3.Distance(_from, _to) * 100f / speed;
            
            transform
                .DOMove(_to, duration)
                .SetEase(Ease.Linear)
                .OnComplete(callback);
        }
    }
}