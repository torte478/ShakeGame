using UnityEngine;
using UnityEngine.Pool;

namespace Shake.Enemies.Enemy.Attack
{
    internal sealed class RemoteAttack : BaseAttack
    {
        private ObjectPool<Bullet> _pool; // TODO: to static
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
        
        protected override void AttackInner(Vector3 target)
        {
            _pool
                .Get()
                .Shot(from: _transform.position, to: target);

            CallFinish();
        }

        private Bullet Create()
        {
            var bullet = Instantiate(prefab, _transform);
            bullet.Deadline += _pool.Release;
            return bullet;
        }
    }
}