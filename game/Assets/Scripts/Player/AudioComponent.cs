using Shake.Utils;
using UnityEngine;

namespace Shake.Player
{
    internal sealed class AudioComponent : MonoBehaviour
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

        public void DoPlay(ShotResultType shot)
        {
            var clip = GetAudioClip(shot);

            clip.To(_audio.PlayOneShot);
        }

        private Maybe<AudioClip> GetAudioClip(ShotResultType shot)
        {
            if (shot == ShotResultType.Misfire)
                return Maybe.Some(misfire);

            if (shot != ShotResultType.Shot)
                return Maybe.None<AudioClip>();

            var random = Random.Range(0, shots.Length);
            var index = random < _previous
                            ? random
                            : (_previous + 1) % shots.Length;
            _previous = index;

            return Maybe.Some(shots[index]);
        }
    }
}