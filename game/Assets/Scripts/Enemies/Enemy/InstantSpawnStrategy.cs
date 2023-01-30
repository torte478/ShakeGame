using DG.Tweening;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal class InstantSpawnStrategy : ISpawnStrategy
    {
        
        public void Spawn(Transform transform, Vector3 target, float speed, TweenCallback callback)
        {
            transform.position = target;
            callback();
        }
    }
}