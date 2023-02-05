using UnityEngine;
using UnityEngine.Pool;

namespace Shake.Enemies.Enemy.Attack
{
    internal sealed class Bullets : MonoBehaviour
    {
        private ObjectPool<Bullet> _pool;
        private Transform _transform;
        
        [SerializeField]
        private Bullet prefab;

        void Awake()
        {
            _transform = GetComponent<Transform>();

            _pool = new ObjectPool<Bullet>(
                createFunc: Create,
                actionOnGet: b => b.gameObject.SetActive(true),
                actionOnRelease: b => b.gameObject.SetActive(false),
                actionOnDestroy: b => b.Deadline -= _pool.Release,
                maxSize: 20);
        }

        public void Shot(Vector3 from, Vector3 to)
            => _pool.Get().Shot(from, to);
        
        private Bullet Create()
        {
            var bullet = Instantiate(prefab, _transform);
            bullet.Deadline += _pool.Release;
            return bullet;
        }
    }
}