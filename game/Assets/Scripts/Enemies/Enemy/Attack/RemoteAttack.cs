using UnityEngine;

namespace Shake.Enemies.Enemy.Attack
{
    internal sealed class RemoteAttack : BaseAttack
    {
        [SerializeField]
        private Bullets.Bullets bullets;

        protected override void Attack(Vector3 target)
        {
            bullets.Shot(from: transform.position, to: target);
            
            CallFinish();
        }
    }
}