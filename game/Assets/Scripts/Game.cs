using UnityEngine;

namespace Shake
{
    internal sealed class Game : MonoBehaviour
    {
        [SerializeField]
        private Player.Player player;

        [SerializeField]
        private Enemies.Enemies enemies;

        void Update()
        {
            var shot = player.Process();
            enemies.Process(shot);
        }
    }
}