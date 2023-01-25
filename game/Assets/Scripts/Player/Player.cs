using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private GunComponent _gunComponent = new();

        void Update()
        {
            var state = _gunComponent.TryShot();
        }
    }
}