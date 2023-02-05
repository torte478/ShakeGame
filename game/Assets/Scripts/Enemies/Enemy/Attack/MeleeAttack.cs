using System;
using System.Collections;
using DG.Tweening;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy.Attack
{
    internal sealed class MeleeAttack : MonoBehaviour, IAttack
    {
        [SerializeField]
        private Transform target;
        
        [SerializeField, Min(Consts.Eps)]
        private float speed;

        [SerializeField, Min(Consts.Eps)]
        private float delay;

        public event Action Finish;

        public void Start()
        {
            StartCoroutine(DelayAttack());
        }

        private IEnumerator DelayAttack()
        {
            yield return new WaitForSeconds(delay);
            
            transform
                .DOTimingMove(target.position, speed)
                .SetEase(Ease.Linear);
        }
    }
}