using UnityEngine;

namespace Shake.Enemies.Enemy.Attack
{
    internal sealed class RemoteAttack : BaseAttack
    {
        private Transform _transform;

        void Awake()
        {
            _transform = GetComponent<Transform>();
        }
        
        protected override void AttackInner(Vector3 target)
        {
            Bullets.Instance.Shot(from: _transform.position, to: target);

            CallFinish();
        }
    }
}