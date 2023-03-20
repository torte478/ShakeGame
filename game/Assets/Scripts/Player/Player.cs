using System;
using Shake.Utils;
using UnityEngine;

namespace Shake.Player
{
    [RequireComponent(typeof(Gun))]
    [RequireComponent(typeof(Audio))]
    internal sealed class Player : MonoBehaviour
    {
        private Gun _gun;
        private Audio _audio;

        public event Action<Vector2> Shot;
        public event Action Dead;
        
        public static Player Instance { get; private set; }

        void Awake()
        {
            _gun = GetComponent<Gun>();
            _audio = GetComponent<Audio>();

            if (Instance is not null)
                throw new Exception("Player instance already defined");

            Instance = this;
        }

        void Start()
        {
            _gun.Fire += OnFire;
        }

        void OnDestroy()
        {
            _gun.Fire -= OnFire;
        }
        
        // ReSharper disable once UnusedParameter.Local
        void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log("Game Over! - Enemy");
            Dead.Call();
        }
        
        private void OnFire(Shot shot)
        {
            _audio.Play(shot);
            shot.Point.To(Shot.Call);
        }
    }
}