using Shake.Creatures;
using Shake.Enemies.Enemy.Attack;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    [RequireComponent(typeof(IAttack))]
    internal sealed class Enemy : Creature
    {
        private Transform _transform;
        private IAttack _attack;
        private Vector3 _target;

        protected override void InnerAwake()
        {
            _transform = GetComponent<Transform>();
            _attack = GetComponent<IAttack>();

            Movement.Step += CheckStartAttack;
            _attack.Finish += Movement.Resume;
        }

        protected override void InnerStart()
        {
            _target = Player.Player.Instance.transform.position;
        }

        protected override void InnerDestroy()
        {
            Movement.Step -= CheckStartAttack;
            _attack.Finish -= Movement.Resume;
        }

        protected override void InnerDeath()
        {
            Movement.Pause();
            _transform.position = Consts.Outside;
        }

        private void CheckStartAttack(int step)
        {
            if (!_attack.IsAttack(step))
                return;

            Movement.Pause();
            _attack.Attack(_target);
        }
    }
}