using System;
using System.Collections.Generic;
using Shake.Enemies.Enemy;
using Shake.Enemies.Enemy.Hp;
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
        
        public event Action<Creature> Death;

        void Awake()
        {
            _hp = GetComponent<IHp>();
            _movement = GetComponent<Movement>();
            
            InnerAwake();
        }

        void Start()
        {
            InnerStart();
        }

        void OnDestroy()
        {
            InnerDestroy();
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
            
            InnerDeath();
            Death.Call(this);
            return true;
        }

        protected virtual void InnerAwake()
        {
        }

        protected virtual void InnerStart()
        {
        }

        protected virtual void InnerDestroy()
        {
        }

        protected virtual void InnerDeath()
        {
        }
    }
}