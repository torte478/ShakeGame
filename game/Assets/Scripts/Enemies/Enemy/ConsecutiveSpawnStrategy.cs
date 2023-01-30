using DG.Tweening;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed class ConsecutiveSpawnStrategy : ISpawnStrategy
    {
        private readonly Vector3 _from;

        public ConsecutiveSpawnStrategy(Vector3 from)
        {
            _from = from;
        }

        public void Spawn(Transform transform, Vector3 target, float speed, TweenCallback callback)
        {
            transform.position = _from;
            
            transform
                .DOTimingMove(target, speed)
                .SetEase(Ease.Linear)
                .OnComplete(callback);
        }
    }
}