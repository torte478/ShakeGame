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

        public void DoShot()
        {
            var state = _gunComponent.DoShot();
            _audioComponent.DoPlay(state.Type);
        }
    }
}