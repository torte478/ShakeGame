using System;
using System.Collections.Generic;
using Shake.Utils;
using UnityEngine;

namespace Shake.Creatures
{
    [RequireComponent(typeof(Hp))]
    [RequireComponent(typeof(Movement))]
    internal abstract class Creature : MonoBehaviour
    {
        private Hp _hp;
        private Movement _movement;

        protected Movement Movement => _movement;
        
        public event Action<Creature> Dead;

        void Awake()
        {
            _hp = GetComponent<Hp>();
            _movement = GetComponent<Movement>();
            
            AwakeInner();
        }

        void Start()
        {
            StartInner();
        }

        void OnDestroy()
        {
            DestroyInner();
        }
        
        public void Init(Vector3 position, IReadOnlyCollection<Vector3> path, float delay)
        {
            _movement!.Init(position, path, delay);
            _hp.Init();
        }
        
        public bool Damage()
        {
            if (!_hp.Damage())
                return false;
            
            StartDeathInner();
            return true;
        }

        protected virtual void AwakeInner()
        {
        }

        protected virtual void StartInner()
        {
        }

        protected virtual void DestroyInner()
        {
        }

        protected virtual void StartDeathInner()
        {
            OnDeathEnd();
        }

        protected virtual void OnDeathEnd()
        {
            Dead.Call(this);
        }
    }
}