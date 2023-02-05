using UnityEngine;

namespace Shake.Enemies.Enemy.Attack
{
    internal sealed class RemoteAttack : BaseAttack
    {
        private Bullets _bullets;
        private Transform _transform;

        void Awake()
        {
            _transform = GetComponent<Transform>();
        }
        
        public override void Init(Bullets bullets)
        {
            _bullets = bullets; //TODO: to singleton
        }

        protected override void AttackInner(Vector3 target)
        {
            _bullets.Shot(from: _transform.position, to: target);

            CallFinish();
        }
    }
}