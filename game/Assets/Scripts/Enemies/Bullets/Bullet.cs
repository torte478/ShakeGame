using System.Collections;
using UnityEngine;

namespace Shake.Enemies.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))] // TODO: add to another
    internal sealed class Bullet : MonoBehaviour
    {
        private Pool _pool;
        private Rigidbody2D _rigidbody;
        private Transform _transform;

        void Awake() // TODO: Start?
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Init(Pool pool, Vector3 position, Vector3 target, float force)
        {
            _pool = pool;
            
            _transform.position = position;

            _rigidbody.AddForce(
                force: (target - position) * force, 
                mode: ForceMode2D.Impulse);

            StartCoroutine(Release());
        }

        private IEnumerator Release()
        {
            yield return new WaitForSeconds(2);
            
            _pool.Release(this);
        }
    }
}