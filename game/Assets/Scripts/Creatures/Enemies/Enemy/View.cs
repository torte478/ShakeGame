using System;
using System.Collections.Generic;
using Shake.Utils;
using UnityEngine;

namespace Shake.Creatures.Enemies.Enemy
{
    [RequireComponent(typeof(Animator))]
    internal sealed class View : MonoBehaviour
    {
        private static readonly Dictionary<Clip, int> Clips =
            new()
            {
                { Clip.Death, Animator.StringToHash("Death") }
            };
        
        private Animator _animator;

        public event Action Dead;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Death()
        {
            _animator.SetBool(Clips[Clip.Death], true);
        }

        public void OnDead()
        {
            Dead.Call();
        }
        
        private enum Clip
        {
            Death
        }
    }
}