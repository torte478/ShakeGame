using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Shake.Creatures.Enemies.Enemy
{
    internal sealed class Bullets : MonoBehaviour
    {
        private ObjectPool<Bullet> _pool;
        private Transform _transform;
        private int _layer;
        
        [SerializeField]
        private Bullet prefab;

        [SerializeField]
        private LayerMask player;

        public static Bullets Instance { get; private set; }

        void Awake()
        {
            if (Instance != null)
                throw new Exception("Singleton already initialized");
            
            Instance = this;
            
            _transform = GetComponent<Transform>();
            _layer = prefab.gameObject.layer;

            _pool = new ObjectPool<Bullet>(
                createFunc: Create,
                actionOnGet: b => b.gameObject.SetActive(true),
                actionOnRelease: b => b.gameObject.SetActive(false),
                actionOnDestroy: b => b.Deadline -= _pool.Release,
                maxSize: 20);
        }

        void Start()
        {
            Player.Player.Instance.Shot += CheckShot;
        }

        private void OnDestroy()
        {
            Player.Player.Instance.Shot -= CheckShot;
        }

        public void Shot(Vector3 from, Vector3 to)
            => _pool.Get().Shot(from, to);
        
        private Bullet Create()
        {
            var bullet = Instantiate(prefab, _transform);
            bullet.gameObject.layer = _layer;
            bullet.Deadline += _pool.Release;
            return bullet;
        }
        
        private void CheckShot(Vector2 position)
        {
            var target = Physics2D.OverlapPoint(position, player);
            if (target == null)
                return;

            if (!target.gameObject.TryGetComponent<Bullet>(out var bullet))
                return;
            
            bullet.Die();
        }
    }
}