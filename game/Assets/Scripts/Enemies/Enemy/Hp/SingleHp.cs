using UnityEngine;

namespace Shake.Enemies.Enemy.Hp
{
    internal sealed class SingleHp : MonoBehaviour, IHp
    {
        public bool Damage() => true;
    }
}