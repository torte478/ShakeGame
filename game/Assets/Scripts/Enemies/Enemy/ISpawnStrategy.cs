using DG.Tweening;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal interface ISpawnStrategy
    {
        void Spawn(Transform transform, TweenCallback callback);
    }
}