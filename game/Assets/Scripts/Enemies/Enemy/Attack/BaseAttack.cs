using System;
using System.Collections;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy.Attack
{
    internal abstract class BaseAttack : MonoBehaviour, IAttack
    {
        [SerializeField, Min(Consts.Eps)]
        private float delay;
        
        [SerializeField]
        private Transform target;
        
        [SerializeField, Min(1)]
        private int frequency;

        public event Action Finish;

        public bool IsAttack(int step)
            => step % frequency == 0;

        public void Start()
            => DelayAttack()
                ._(StartCoroutine);

        protected abstract void Attack(Vector3 target);

        protected void CallFinish()
            => Finish.Call();
        
        private IEnumerator DelayAttack()
        {
            yield return new WaitForSeconds(delay);

            Attack(target.position);
        }
    }
}