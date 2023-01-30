using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed partial class Enemy : MonoBehaviour
    {
        private int _hp;
        private Vector3[] _path;
        private Maybe<Sequence> _movement;

        public State EnemyState { get; private set; } = State.Start;
        public EnemyConfig EnemyConfig { get; private set; }

        // TODO: shit
        public void Init(EnemyConfig config, IReadOnlyCollection<Vector3> path)
        {
            EnemyConfig = config;
            _path = path.ToArray();
            _movement = Maybe.None<Sequence>();
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
                              OnSpawnComplete();
                              callback();
                          });
        }

        private void OnSpawnComplete()
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
                    ._(sequence.Append);
            }
            sequence.SetLoops(-1);


            _movement = Maybe.Some(sequence);
        }

        public bool Hurt()
        {
            --_hp;
            if (_hp > 0)
                return false;

            EnemyState = State.Dead;

            _movement.Match(seq => seq.Pause());
            transform.position = EnemyConfig.Spawn;

            return true;
        }
    }
}