using DG.Tweening;
using UnityEngine;

namespace Shake.Enemies
{
    internal class InstantSpawnStrategy : ISpawnStrategy
    {
        private readonly Vector3 _position;
        
        public InstantSpawnStrategy(Vector3 position)
        {
            _position = position;
        }

        public void Spawn(Transform transform, TweenCallback callback)
        {
            transform.position = _position;
            callback();
        }
    }
}