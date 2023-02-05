using System;
using DG.Tweening;
using Shake.Utils;
using UnityEngine;

namespace Shake.Enemies.Enemy
{
    internal sealed class MovementComponent
    {
        private readonly Vector3[] _path;
        
        private Maybe<Sequence> _movement;
        private int _step;

        public Vector3 Begin => _path[0];

        public event Action<int> Step;

        public MovementComponent(Vector3[] path)
        {
            _path = path;
            _step = 0;
            _movement = Maybe.None<Sequence>();
        }

        public void Pause()
        {
            _movement.To(_ => _.Pause());
        }

        public void Resume()
        {
            _movement.To(_ => _.Play());
        }

        public void Start(Transform transform, float speed)
        {
            var sequence = DOTween.Sequence();
            for (var i = 0; i < _path.Length; ++i)
            {
                transform
                    .DOTimingMove(
                        from: i > 0 ? _path[i - 1] : transform.position,
                        to: _path[i],
                        speed: speed)
                    .SetEase(Ease.Linear)
                    .OnComplete(IncrementStepIndex)
                    ._(sequence.Append);
            }
            sequence.SetLoops(-1);

            _movement = Maybe.Some(sequence);
        }

        private void IncrementStepIndex()
        {
            _step = _step == int.MaxValue
                        ? 0
                        : _step + 1;
            
            Step.Call(_step);
        }
    }
}