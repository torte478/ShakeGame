using System;
using System.Collections;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))] // TODO: add to another
    internal sealed class Bullet : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody2D _rigidbody;

        [SerializeField, Min(0.01f)]
        private float force;

        public event Action<Bullet> Deadline;

        void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Shot(Vector3 from, Vector3 to)
        {
            _transform.position = from;

            _rigidbody.AddForce(
                force: (to - from) * force, 
                mode: ForceMode2D.Impulse);

            StartCoroutine(WaitDeadline());
        }

        private IEnumerator WaitDeadline()
        {
            yield return new WaitForSeconds(2);

            Deadline.Call(this);
        }
    }
}