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

        public event Action<Vector3> Shot;

        void Start()
        {
            _gun = GetComponent<Gun>();
            _audio = GetComponent<Audio>();
        }

        void Update()
        {
            var shot = _gun.DoShot();
                
            shot.To(_audio.Play);
            shot.To(_ => _.Point.To(Shot.Call));
        }
    }
}