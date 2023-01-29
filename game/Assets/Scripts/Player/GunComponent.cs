using System;
using UnityEngine;

namespace Player
{
    internal sealed class GunComponent
    {
        private readonly Zones _zones;
        private readonly Camera _camera;
        
        private bool _isLeft = true;
        private Func<Vector2, Zone, ShotResult> _doShot;

        public GunComponent(Zones zones, Camera camera)
        {
            _zones = zones;
            _camera = camera;
            _doShot = DoFirstShot;
        }

        public ShotResult TryShot()
        {
            if (!Input.GetMouseButtonDown(0))
                return new ShotResult(ShotResultType.None);

            var cursor = _camera.ScreenToWorldPoint(Input.mousePosition);
            var zone = _zones.ToZone(cursor);

            return _doShot(cursor, zone);
        }

        private ShotResult DoFirstShot(Vector2 cursor, Zone zone)
        {
            var isLeft = zone is Zone.Left or Zone.Center;
            var result = new ShotResult(ShotResultType.Shot, cursor, isLeft);

            _isLeft = !isLeft;
            _doShot = DoShot;
            
            return result;
        }

        private ShotResult DoShot(Vector2 cursor, Zone zone)
        {
            var isShot = (zone == Zone.Left && _isLeft) 
                         || (zone == Zone.Right && !_isLeft);
            if (!isShot)
                return new ShotResult(ShotResultType.Misfire);

            var result = new ShotResult(ShotResultType.Shot, cursor, _isLeft); 
            _isLeft = !_isLeft;
            return result;
        }
    }
}