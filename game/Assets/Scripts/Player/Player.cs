using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private GunComponent _gunComponent;

        [SerializeField]
        private Zones zones;

        void Start()
        {
            _gunComponent = new GunComponent(
                zones: zones,
                camera: Camera.main);
        }

        void Update()
        {
            _gunComponent.TryShot();
        }
    }
}