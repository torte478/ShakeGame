using UnityEngine;

namespace Shake.Zones
{
    internal sealed class Zones : MonoBehaviour
    {
        [SerializeField, Min(1f)]
        private float width = 1f;

        [SerializeField]
        private View view;

        void Start()
        {
            view.Init(width);
        }
        
        public Zone ToZone(Vector3 point)
        {
            if (point.x < -width / 2f)
                return Zone.Left;

            if (point.x > width / 2f)
                return Zone.Right;

            return Zone.Center;
        }
    }
}