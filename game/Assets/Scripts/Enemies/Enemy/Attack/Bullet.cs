using System;
using System.Collections;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy.Attack
{
    [RequireComponent(typeof(Rigidbody2D))] // TODO: add to another
    internal sealed class Bullet : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody2D _rigidbody;

        [SerializeField, Min(Consts.Eps)]
        private float force;

        [SerializeField, Min(1f)]
        private float deadline;

        public event Action<Bullet> Deadline;

        void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Shot(Vector3 from, Vector3 to)
        {
            _transform.position = from;

            var direction = (to - from).normalized;
            _rigidbody.AddForce(
                force: direction * force, 
                mode: ForceMode2D.Impulse);

            StartCoroutine(WaitDeadline());
        }

        private IEnumerator WaitDeadline()
        {
            yield return new WaitForSeconds(deadline);

            Deadline.Call(this);
        }
    }
}