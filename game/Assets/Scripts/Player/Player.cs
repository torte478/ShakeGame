using System;
using Shake.Utils;
using UnityEngine;

namespace Shake.Player
{
    internal sealed class Player : MonoBehaviour
    {
        private GunComponent _gunComponent;
        private AudioComponent _audioComponent;

        public event Action<Vector2> Shot;

        void Start()
        {
            _gunComponent = GetComponent<GunComponent>();
            _audioComponent = GetComponent<AudioComponent>();
        }

        void Update()
        {
            var state = _gunComponent.DoShot();
            _audioComponent.DoPlay(state.Type);

            if (state.Type == ShotResultType.Shot)
                Shot.Call(state.Point.Value);
        }
    }
}