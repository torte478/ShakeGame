using UnityEngine;

namespace Shake.Creatures
{
    internal sealed class Hp : MonoBehaviour
    {
        private int _hp;

        [SerializeField, Min(1)]
        private int hp;

        public void Init()
            => _hp = hp;
        
        public bool Damage()
            => --_hp <= 0;
    }
}