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
        
        [SerializeField, Min(1)]
        private int frequency;

        public event Action Finish;

        public bool IsAttack(int step)
            => step % frequency == 0;

        public void Attack(Vector3 target)
            => DelayAttack(target)
                ._(StartCoroutine);
        
        public virtual void Init(Bullets bullets) {}

        protected abstract void AttackInner(Vector3 target);

        protected void CallFinish()
            => Finish.Call();
        
        private IEnumerator DelayAttack(Vector3 target)
        {
            yield return new WaitForSeconds(delay);

            AttackInner(target);
        }
    }
}