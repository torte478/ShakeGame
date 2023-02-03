using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    // TODO: refactor
    internal sealed partial class Enemy : MonoBehaviour
    {
        private Vector3[] _path;
        private int _hp;
        private Maybe<Sequence> _movement;
        private int _step;
        private IAttack _attack;

        public State EnemyState { get; private set; } = State.Start;
        public EnemyConfig EnemyConfig { get; private set; }

        // TODO: shit
        public void Init(EnemyConfig config, Bullets.Bullets bullets, IReadOnlyCollection<Vector3> path)
        {
            EnemyConfig = config;
            _path = path.ToArray();
            _step = 0;
            _movement = Maybe.None<Sequence>();
            _attack = config.AttackType switch
            {
                AttackType.Remote => new RemoteAttack(bullets, config.AttackDelay, config.Target),
                AttackType.Melee => new MeleeAttack(config.Target, config.AttackSpeed),

                _ => throw new Exception($"Unexpected attack type: {config.AttackType}")
            };
        }

        public void Spawn(ISpawnStrategy strategy, Action callback)
        {
            _hp = EnemyConfig.Hp;
            
            EnemyState = State.Spawn;
            strategy.Spawn(
                transform: transform,
                target: _path[0],
                speed: EnemyConfig.Speed, 
                callback: () =>
                          {
                              StartMovement();
                              callback();
                          });
        }
        
        public bool Hurt()
        {
            --_hp;
            if (_hp > 0)
                return false;

            EnemyState = State.Dead;

            _movement.To(_ => _.Pause());
            transform.position = Consts.Outside;

            return true;
        }

        private void StartMovement()
        {
            EnemyState = State.Ready;

            var sequence = DOTween.Sequence();
            for (var i = 0; i < _path.Length; ++i)
            {
                transform
                    .DOTimingMove(
                        from: i > 0 ? _path[i - 1] : transform.position,
                        to: _path[i],
                        speed: EnemyConfig.Speed)
                    .SetEase(Ease.Linear)
                    .OnComplete(OnMovementStepComplete)
                    ._(sequence.Append);
            }
            sequence.SetLoops(-1);

            _movement = Maybe.Some(sequence);
        }

        private void OnMovementStepComplete()
        {
            ++_step;
            if (_step < EnemyConfig.Attack)
                return;

            _step = 0;
            _movement.To(_ => _.Pause());

            var attack = _attack.Start(
                transform: transform,
                callback: () => _movement.To(_ => _.Play()));
            
            StartCoroutine(attack);
        }
    }
}