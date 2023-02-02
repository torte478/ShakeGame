using System.Collections.Generic;
using UnityEngine;

namespace Shake.Enemies.Bullets
{
    internal sealed class Pool : MonoBehaviour
    {
        private readonly Stack<Bullet> _bullets = new();

        private Transform _transform;

        [SerializeField]
        private Bullet prefab;

        [SerializeField]
        private Transform player;

        [SerializeField, Min(0f)]
        private float force;

        void Start()
        {
            _transform = GetComponent<Transform>();
        }
        
        public Bullet Spawn(Vector3 position)
        {
            var bullet = GetBullet();
            bullet.gameObject.SetActive(true);
            bullet.Init(this, position, player.position, force);
            return bullet;
        }

        public void Release(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            _bullets.Push(bullet);
        }

        private Bullet GetBullet()
        {
            if (_bullets.Count > 0)
                return _bullets.Pop();

            return Instantiate(prefab, _transform).GetComponent<Bullet>();
        }
    }
}