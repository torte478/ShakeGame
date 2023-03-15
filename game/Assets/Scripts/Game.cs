using UnityEngine;

namespace Shake
{
    [RequireComponent(typeof(AudioSource))]
    internal sealed class Game : MonoBehaviour
    {
        private AudioSource _audio;
        private Player.Player _player;
        
        [SerializeField]
        private AudioClip loss;

        void Start()
        {
            _audio = GetComponent<AudioSource>();
            _player = Player.Player.Instance;
            
            _player.Dead += GameOver;
        }

        void OnDestroy()
        {
            _player.Dead -= GameOver;
        }

        private void GameOver()
        {
            _audio.PlayOneShot(loss);
        }
    }
}