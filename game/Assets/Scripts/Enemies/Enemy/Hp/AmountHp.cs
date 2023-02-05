using UnityEngine;

namespace Shake.Enemies.Enemy.Hp
{
    internal sealed class AmountHp : MonoBehaviour, IHp
    {
        private int _hp;

        [SerializeField, Min(1)]
        private int hp;

        void Awake()
        {
            _hp = hp;
        }

        public bool Damage()
            => --_hp <= 0;
    }
}