using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private GunComponent _gunComponent;
        private AudioComponent _audioComponent;

        void Start()
        {
            _gunComponent = GetComponent<GunComponent>();
            _audioComponent = GetComponent<AudioComponent>();
        }

        void Update()
        {
            var state = _gunComponent.TryShot();
            _audioComponent.Play(state.Type);
        }
    }
}