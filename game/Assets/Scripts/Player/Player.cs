using Shake.Utils;
using UnityEngine;

namespace Shake.Player
{
    internal sealed class Player : MonoBehaviour
    {
        private GunComponent _gunComponent;
        private AudioComponent _audioComponent;

        void Start()
        {
            _gunComponent = GetComponent<GunComponent>();
            _audioComponent = GetComponent<AudioComponent>();
        }

        public Maybe<Vector2> Process()
        {
            var state = _gunComponent.DoShot();
            _audioComponent.DoPlay(state.Type);

            return state.Point;
        }
    }
}