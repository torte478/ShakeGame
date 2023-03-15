using UnityEngine;

namespace Shake.Player
{
    [RequireComponent(typeof(AudioSource))]
    internal sealed class Audio : MonoBehaviour
    {
        private AudioSource _audio;
        
        private int _previous;

        [SerializeField]
        private AudioClip misfire;

        [SerializeField]
        private AudioClip[] shots;

        void Start()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void Play(Shot shot)
        {
            var clip = shot.Point.To(
                _ => GetRandomShot(),
                () => misfire);

            _audio.PlayOneShot(clip);
        }

        private AudioClip GetRandomShot()
        {
            var random = Random.Range(0, shots.Length);
            var index = random < _previous
                            ? random
                            : (_previous + 1) % shots.Length;
            _previous = index;

            return shots[index];
        }
    }
}