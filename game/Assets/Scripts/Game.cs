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
            player.DoShot();
            enemies.DoSpawn();
        }
    }
}