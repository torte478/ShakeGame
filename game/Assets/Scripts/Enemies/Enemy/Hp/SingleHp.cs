using UnityEngine;

namespace Shake.Enemies.Enemy.Hp
{
    internal sealed class SingleHp : MonoBehaviour, IHp
    {
        public void Init() {}
        
        public bool Damage() => true;
    }
}