using System;
using Shake.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shake.Zones
{
    internal sealed class Zones : MonoBehaviour
    {
        [SerializeField, Min(1f)]
        private float width = 1f;

        [SerializeField]
        private View view;
        
        [SerializeField]
        private Rect area;
        
        [SerializeField]
        private float spawnY;

        void Start()
        {
            view.Init(width);
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                center: new Vector3(area.center.x, area.center.y, Consts.Eps),
                size: new Vector3(area.size.x, area.size.y, Consts.Eps));
        }
        
        public Zone ToZone(Vector3 point)
        {
            if (point.x < -width / 2f)
                return Zone.Left;

            if (point.x > width / 2f)
                return Zone.Right;

            return Zone.Center;
        }
        
        public Vector2 ToPoint( bool isSpawn = false, Zone zone = Zone.All)
        {
            var xRange = zone switch
            {
                Zone.LeftHalf => (area.xMin, 0),
                Zone.RightHalf => (0, area.xMax),
                Zone.All => (area.xMin, area.xMax),
                _ => throw new Exception($"Wrong zone value: {zone}")
            };
            var x = Random.Range(xRange.xMin, xRange.xMax);

            var y = isSpawn
                        ? spawnY
                        : Random.Range(area.yMin, area.yMax);

            return new Vector2(x, y);
        }
    }
}