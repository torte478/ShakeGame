using DG.Tweening;
using UnityEngine;

namespace Shake.Enemies
{
    internal interface ISpawnStrategy
    {
        void Spawn(Transform transform, TweenCallback callback);
    }
}