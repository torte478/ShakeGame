using System.Collections.Generic;
using UnityEngine;

namespace Shake.Enemies.Bullets
{
    internal sealed class Pool : MonoBehaviour
    {
        private readonly Stack<Bullet> _bullets = new();

        [SerializeField]
        private Bullet prefab;
        
        public Bullet Get(Vector3 position, Vector3 target, float speed)
        {
            var bullet = GetBullet();
            bullet.Init(this, position, target, speed);
            bullet.gameObject.SetActive(true);
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

            return Instantiate(prefab).GetComponent<Bullet>();
        }
    }
}