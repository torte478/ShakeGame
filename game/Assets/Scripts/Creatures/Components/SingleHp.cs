using UnityEngine;

namespace Shake.Creatures.Components
{
    // TODO: to AmountHp
    internal sealed class SingleHp : MonoBehaviour, IHp
    {
        public void Init() {}
        
        public bool Damage() => true;
    }
}