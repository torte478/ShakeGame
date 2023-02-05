using DG.Tweening;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy.Attack
{
    internal sealed class MeleeAttack : BaseAttack
    {
        [SerializeField, Min(Consts.Eps)]
        private float speed;

        protected override void Attack(Vector3 target)
            => transform
               .DOTimingMove(target, speed)
               .SetEase(Ease.Linear); 
    }
}