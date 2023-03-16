using Shake.Utils;
using UnityEngine;

namespace Shake.Creatures.Enemies.Enemy
{
    [RequireComponent(typeof(IAttack))]
    internal sealed class Enemy : Creature
    {
        private Transform _transform;
        private IAttack _attack;
        private Vector3 _target;

        [SerializeField]
        private View view;

        protected override void AwakeInner()
        {
            _transform = GetComponent<Transform>();
            _attack = GetComponent<IAttack>();

            Movement.Step += CheckStartAttack;
            _attack.Finish += Movement.Resume;
        }

        protected override void StartInner()
        {
            _target = Player.Player.Instance.transform.position;
            view.Dead += OnDeathEnd;
        }

        protected override void DestroyInner()
        {
            Movement.Step -= CheckStartAttack;
            _attack.Finish -= Movement.Resume;
            view.Dead -= OnDeathEnd;
        }

        protected override void StartDeathInner()
        {
            Movement.Pause();
            view.Death();
        }

        protected override void OnDeathEnd()
        {
            _transform.position = Consts.Outside;
            base.OnDeathEnd();
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