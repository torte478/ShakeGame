using UnityEngine;

namespace Shake.Area
{
    public class Zones : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 10)]
        private float width = 1f;

        [SerializeField]
        private Transform prefab;

        [SerializeField]
        private bool show;

        void Start()
        {
            if (!show)
                return;

            var center = Instantiate(prefab, GetComponent<Transform>());

            var scale = center.localScale;
            scale.x = width;
            center.localScale = scale;
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