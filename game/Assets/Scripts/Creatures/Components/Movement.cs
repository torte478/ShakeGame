using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Shake.Utils;
using UnityEngine;

namespace Shake.Creatures.Components
{
    internal sealed class Movement : MonoBehaviour
    {
        private Sequence _movement;
        private int _step;
        private Transform _transform;

        [SerializeField, Min(Consts.Eps)]
        private float speed;

        public event Action<int> Step;

        void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void Init(Vector3 start, IReadOnlyCollection<Vector3> path, float delay)
        {
            _step = 0;
            transform.position = start;
            _movement?.Pause();
            
            _movement = DOTween.Sequence();
            _movement.Append(BuildStep(start, path.First()));

            var loop = DOTween.Sequence();
            foreach (var (from, to) in EnumerateCyclicPairs(path))
                BuildStep(from, to, delay)
                    .OnComplete(IncrementStepIndex)
                    ._(loop.Append);
            
            loop.SetLoops(int.MaxValue);

            _movement.Append(loop);
        }
        
        public void Pause()
            => _movement.Pause();

        public void Resume()
            => _movement.Play();

        private TweenerCore<Vector3, Vector3, VectorOptions> BuildStep(
            Vector3 from,
            Vector3 to,
            float delay = 0f)
            => _transform
               .DOTimingMove(from, to, speed)
               .SetDelay(delay)
               .SetEase(Ease.Linear);

        private void IncrementStepIndex()
        {
            _step = _step == int.MaxValue
                        ? 0
                        : _step + 1;
            
            Step.Call(_step);
        }
        
        private static IEnumerable<(Vector3 From, Vector3 To)> EnumerateCyclicPairs(
            IReadOnlyCollection<Vector3> points)
        {
            var first = points.First();
            
            var previous = first;
            foreach (var current in points.Skip(1))
            {
                yield return (previous, current);
                previous = current;
            }

            yield return (previous, first);
        }
    }
}