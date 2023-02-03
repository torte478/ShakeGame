using UnityEngine;

namespace Shake.Area
{
    internal sealed class Area : MonoBehaviour
    {
        private Rect _npcArea; // TODO: rename
        
        [SerializeField, Min(1f)]
        private float width = 1f;

        [SerializeField]
        private Transform prefab;

        [SerializeField]
        private bool showCenter;

        [SerializeField]
        private float spawnY;

        [SerializeField]
        private float areaOffset = 1f;

        [SerializeField]
        private float areaBottom;

        public Vector3 Spawn => new(0, spawnY);

        void Start()
        {
            var view = Camera.main!;
            
            var begin = view.ScreenToWorldPoint(new Vector3(0, view.pixelHeight));
            var end = new Vector3(
                x: view.ScreenToWorldPoint(new Vector3(view.pixelWidth, 0)).x,
                y: areaBottom);
            
            _npcArea = new Rect(
                x: begin.x + areaOffset,
                y: begin.y - areaOffset,
                width: end.x - begin.x - 2 * areaOffset,
                height: end.y - begin.y - 2 * areaOffset);

            ShowCenter();
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                new Vector3(_npcArea.center.x, _npcArea.center.y, 0.01f),
                new Vector3(_npcArea.size.x, _npcArea.size.y, 0.01f));
        }

        private void ShowCenter()
        {
            if (!showCenter)
                return;
            
            var center = Instantiate(prefab,  GetComponent<Transform>());
            center.transform.position = new Vector3(0, 0, 10);

            var scale = center.localScale;
            scale.x = width;
            center.localScale = scale;
        }

        public Region ToRegion(Vector3 point)
        {
            if (point.x < -width / 2f)
                return Region.Left;

            if (point.x > width / 2f)
                return Region.Right;

            return Region.Center;
        }

        public Vector3 ToPoint(bool isSpawn = false, Half area = Half.Any)
        {
            var xRange = area switch
            {
                Half.Left => (_npcArea.xMin, 0),
                Half.Right => (0, _npcArea.xMax),
                _ => (_npcArea.xMin, _npcArea.xMax)
            };
            var x = Random.Range(xRange.xMin, xRange.xMax);
            
            var y = isSpawn
                        ? spawnY
                        : Random.Range(_npcArea.yMin, _npcArea.yMax);

            return new Vector3(x, y);
        }
    }
}        