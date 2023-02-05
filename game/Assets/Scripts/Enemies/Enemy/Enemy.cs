using System;
using System.Collections.Generic;
using System.Linq;
using Shake.Enemies.Enemy.Attack;
using Shake.Enemies.Enemy.Hp;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed class Enemy : MonoBehaviour
    {
        private IHp _hp;
        private IAttack _attack;
        private MovementComponent _movement;

        public EnemyStateType EnemyStateType { get; private set; } = EnemyStateType.Start;
        public EnemyConfig EnemyConfig { get; private set; }

        void Awake()
        {
            _hp = GetComponent<IHp>();
            _attack = GetComponent<IAttack>();
        }

        void OnDestroy()
        {
            if (_movement is null)
                return;

            _movement.Step -= CheckStartAttack;
            _attack.Finish -= _movement.Resume;
        }

        // TODO: shit
        public void Init(EnemyConfig config, IReadOnlyCollection<Vector3> path)
        {
            EnemyConfig = config;
            
            if (_movement is not null)
            {
                _movement.Step -= CheckStartAttack;
                _attack.Finish -= _movement.Resume;    
            }
            
            _movement = new MovementComponent(path.ToArray());

            _movement.Step += CheckStartAttack;
            _attack.Finish += _movement.Resume;
        }

        public void Spawn(ISpawnStrategy strategy, Action callback)
        {
            EnemyStateType = EnemyStateType.Spawn;
            strategy.Spawn(
                transform: transform,
                target: _movement.Begin,
                speed: EnemyConfig.Speed, 
                callback: () =>
                          {
                              StartMovement();
                              callback();
                          });
        }

        public bool Hurt()
        {
            if (!_hp.Damage())
                return false;

            EnemyStateType = EnemyStateType.Dead;

            _movement.Pause();
            transform.position = Consts.Outside;

            return true;
        }

        private void StartMovement()
        {
            EnemyStateType = EnemyStateType.Ready;
            _movement.Start(transform, EnemyConfig.Speed);
        }

        private void CheckStartAttack(int step)
        {
            if (step % EnemyConfig.Attack != 0)
                return;

            _movement.Pause();
            _attack.Start();
        }
    }
}