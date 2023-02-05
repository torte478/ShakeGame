using System.Collections.Generic;
using Shake.Enemies.Enemy.Attack;
using Shake.Enemies.Enemy.Hp;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    [RequireComponent(typeof(IAttack))]
    [RequireComponent(typeof(IHp))]
    [RequireComponent(typeof(Movement))]
    internal sealed class Enemy : MonoBehaviour
    {
        private Transform _transform;
        private IHp _hp;
        private IAttack _attack;
        private Movement _movement;

        void Awake()
        {
            _transform = GetComponent<Transform>();
            _hp = GetComponent<IHp>();
            _attack = GetComponent<IAttack>();
            _movement = GetComponent<Movement>();

            _movement.Step += CheckStartAttack;
            _attack.Finish += _movement.Resume;
        }

        void OnDestroy()
        {
            _movement.Step -= CheckStartAttack;
            _attack.Finish -= _movement.Resume;
        }

        public void Init(Vector3 position, IReadOnlyCollection<Vector3> path)
        {
            _movement!.Init(position, path);
            _hp.Init();
        }

        public bool Damage()
        {
            if (!_hp.Damage())
                return false;

            Death();
            return true;
        }

        private void CheckStartAttack(int step)
        {
            if (!_attack.IsAttack(step))
                return;

            _movement.Pause();
            _attack.Start();
        }

        private void Death()
        {
            _movement.Pause();
            _transform.position = Consts.Outside;
        }
    }
}