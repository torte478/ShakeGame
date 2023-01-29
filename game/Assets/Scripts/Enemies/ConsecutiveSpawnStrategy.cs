using DG.Tweening;
using UnityEngine;

namespace Shake.Enemies
{
    internal sealed class ConsecutiveSpawnStrategy : ISpawnStrategy
    {
        private readonly Vector3 _from;
        private readonly Vector3 _to;
        private readonly float _speed;

        public ConsecutiveSpawnStrategy(Vector3 from, Vector3 to, float speed)
        {
            _from = from;
            _to = to;
            _speed = speed;
        }

        public void Spawn(Transform transform, TweenCallback callback)
        {
            transform.position = _from;
            var duration = Vector3.Distance(_from, _to) * 1000f / _speed;
            
            Debug.Log(duration);
            
            transform
                .DOMove(_to, duration)
                .OnComplete(callback);
        }
    }
}