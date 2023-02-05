using System;
using System.Collections;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy.Attack
{
    internal sealed class RemoteAttack : MonoBehaviour, IAttack
    {
        [SerializeField]
        private Bullets.Bullets bullets;
        
        [SerializeField, Min(Consts.Eps)]
        private float delay;
        
        [SerializeField]
        private Transform target;
        
        public event Action Finish;

        public void Start()
        {
            StartCoroutine(DelayAttack());
        }

        private IEnumerator DelayAttack()
        {
            yield return new WaitForSeconds(delay);

            bullets.Shot(transform.position, target.position);

            Finish.Call();
        }
    }
}