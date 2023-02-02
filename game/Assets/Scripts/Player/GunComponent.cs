using System;
using Shake.Area;
using Shake.Contracts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shake.Player
{
    internal sealed class GunComponent : MonoBehaviour
    {
        private Camera _camera;
        
        private bool _isLeft = true;
        private Func<Vector2, Region, ShotResult> _shot;
        
        [FormerlySerializedAs("zones")]
        [SerializeField]
        private Area.Area area;

        public GunComponent()
        {
            _shot = DoFirstShot;
        }

        void Start()
        {
            _camera = Camera.main;
        }

        public ShotResult DoShot()
        {
            if (!Input.GetMouseButtonDown(0))
                return new ShotResult(ShotResultType.None);

            var cursor = _camera.ScreenToWorldPoint(Input.mousePosition);
            var zone = area.ToRegion(cursor);

            return _shot(cursor, zone);
        }

        private ShotResult DoFirstShot(Vector2 cursor, Region region)
        {
            var isLeft = region is Region.Left or Region.Center;
            var result = new ShotResult(ShotResultType.Shot, cursor, isLeft);

            _isLeft = !isLeft;
            _shot = DoShot;
            
            return result;
        }

        private ShotResult DoShot(Vector2 cursor, Region region)
        {
            var isShot = (region == Region.Center)
                         || (region == Region.Left && _isLeft) 
                         || (region == Region.Right && !_isLeft);
            if (!isShot)
                return new ShotResult(ShotResultType.Misfire);

            var result = new ShotResult(ShotResultType.Shot, cursor, _isLeft); 
            _isLeft = !_isLeft;
            return result;
        }
    }
}