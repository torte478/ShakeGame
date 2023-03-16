using System;
using System.Collections.Generic;
using Shake.Creatures.Components;
using Shake.Utils;
using UnityEngine;

namespace Shake.Creatures
{
    [RequireComponent(typeof(IHp))]
    [RequireComponent(typeof(Movement))]
    internal abstract class Creature : MonoBehaviour
    {
        private IHp _hp;
        private Movement _movement;

        protected Movement Movement => _movement;
        
        public event Action<Creature> Dead;

        void Awake()
        {
            _hp = GetComponent<IHp>();
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
        
        public void Init(Vector3 position, IReadOnlyCollection<Vector3> path)
        {
            _movement!.Init(position, path);
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