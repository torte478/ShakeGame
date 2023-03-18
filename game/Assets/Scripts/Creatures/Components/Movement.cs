using System;
using System.Collections;
using System.Collections.Generic;
using Shake.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Shake.Creatures.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    internal sealed class Movement : MonoBehaviour
    {
        private readonly Memorable<State> _state = new(State.Begin);
        
        private Transform _transform;
        
        private NavMeshAgent _navigation;
        
        private int _step;
        private float _delay;

        private IEnumerator<Vector3> _path;

        [SerializeField, Min(Consts.Eps)]
        private float speed;

        public event Action<int> Step;

        void Awake()
        {
            _transform = GetComponent<Transform>();

            _navigation = GetComponent<NavMeshAgent>();

            _navigation.updateRotation = false;
            _navigation.updateUpAxis = false;
            _navigation.speed = speed;
        }

        void Update()
        {
            if (_state.Current != State.Move)
                return;

            if (Vector2.Distance(_transform.position, _path.Current) > Consts.Eps)
                return;
            
            _step = _step == int.MaxValue
                        ? 0
                        : _step + 1;
            Step.Call(_step);
            
            _state.Current = State.Wait;
            StartCoroutine(NextStep());
        }

        private IEnumerator NextStep()
        {
            yield return new WaitForSeconds(_delay);
            
            _path.MoveNext();
            _navigation.SetDestination(_path.Current);
            _state.Current = State.Move;
        }

        public void Init(Vector3 start, IReadOnlyCollection<Vector3> path, float delay)
        {
            _step = 0;
            _delay = delay;

            _path = BuildPath(path).GetEnumerator();
            _path.MoveNext();

            _navigation.enabled = true;
            _navigation.Warp(start);
            _navigation.SetDestination(_path.Current);
            _state.Current = State.Move;
        }

        public void Pause()
        {
            _state.Current = State.Pause;
            _navigation.isStopped = true;
        }

        public void Resume()
        {
            _state.Current = _state.Previous;
            _navigation.isStopped = false;
        }

        private static IEnumerable<Vector3> BuildPath(IReadOnlyCollection<Vector3> path)
        {
            while (true)
                foreach (var point in path)
                    yield return point;
            
            // ReSharper disable once IteratorNeverReturns
        }

        private enum State
        {
            Begin,
            Move,
            Wait,
            Pause
        };
    }
}