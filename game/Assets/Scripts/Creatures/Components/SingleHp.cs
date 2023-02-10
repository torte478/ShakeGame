using UnityEngine;

namespace Shake.Creatures.Components
{
    internal sealed class SingleHp : MonoBehaviour, IHp
    {
        public void Init() {}
        
        public bool Damage() => true;
    }
}