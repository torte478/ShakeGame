using DG.Tweening;
using Shake.Utils;
using UnityEngine;

namespace Shake.Creatures.Enemies.Enemy
{
    internal sealed class MeleeAttack : BaseAttack
    {
        [SerializeField, Min(Consts.Eps)]
        private float speed;

        protected override void AttackInner(Vector3 target)
            => transform
               .DOTimingMove(target, speed)
               .SetEase(Ease.Linear); 
    }
}