using UnityEngine;

namespace Shake
{
    internal sealed class Game : MonoBehaviour
    {
        [SerializeField]
        private Player.Player player;

        [SerializeField]
        private Enemies.Enemies enemies;

        void Start()
        {
            enemies.Init(player.transform.position);

            player.Shot += enemies.CheckDamage;
        }

        private void OnDestroy()
        {
            player.Shot -= enemies.CheckDamage;
        }
    }
}