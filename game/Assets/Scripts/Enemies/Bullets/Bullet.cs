using System.Collections;
using UnityEngine;

namespace Shake.Enemies.Bullets
{
    internal sealed class Bullet : MonoBehaviour
    {
        private Pool _pool;

        public void Init(Pool pool, Vector3 position, Vector3 target, float speed)
        {
            _pool = pool;
            
            // movement

            StartCoroutine(Release());
        }

        private IEnumerator Release()
        {
            yield return new WaitForSeconds(2);
            
            _pool.Release(this);
        }
    }
}