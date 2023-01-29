using UnityEngine;

namespace Player
{
    internal sealed class AudioComponent : MonoBehaviour
    {
        private AudioSource _source;

        [SerializeField]
        private AudioClip misfire;

        void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        public void Play(ShotResultType shot)
        {
            if (shot == ShotResultType.Misfire)
                _source.PlayOneShot(misfire);
        }
    }
}