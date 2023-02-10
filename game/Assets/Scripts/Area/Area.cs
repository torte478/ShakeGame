using Shake.Utils;
using UnityEngine;

namespace Shake.Area
{
    internal sealed class Area : MonoBehaviour
    {
        [SerializeField]
        private Rect area;
        
        [SerializeField]
        private float spawnY;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                center: new Vector3(area.center.x, area.center.y, Consts.Eps),
                size: new Vector3(area.size.x, area.size.y, Consts.Eps));
        }

        public Vector2 ToPoint( bool isSpawn = false, Region region = Region.Any)
        {
            var xRange = region switch
            {
                Region.Left => (area.xMin, 0),
                Region.Right => (0, area.xMax),
                _ => (area.xMin, area.xMax)
            };
            var x = Random.Range(xRange.xMin, xRange.xMax);

            var y = isSpawn
                        ? spawnY
                        : Random.Range(area.yMin, area.yMax);

            return new Vector2(x, y);
        }
    }
}        