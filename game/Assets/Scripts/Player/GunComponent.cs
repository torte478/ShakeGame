using UnityEngine;

namespace Player
{
    internal sealed class GunComponent
    {
        private readonly Zones _zones;
        private readonly Camera _camera;
        
        private bool _isLeft = true;

        public GunComponent(Zones zones, Camera camera)
        {
            _zones = zones;
            _camera = camera;
        }

        public ShotResult TryShot()
        {
            if (!Input.GetMouseButtonDown(0))
                return new ShotResult(ShotResultType.None);

            var cursor = _camera.ScreenToWorldPoint(Input.mousePosition);
            var zone = _zones.ToZone(cursor);

            var isShot = (zone == Zone.Left && _isLeft) 
                         || (zone == Zone.Right && !_isLeft);
            if (!isShot)
                return new ShotResult(ShotResultType.Misfire);

            _isLeft = !_isLeft;
            return new ShotResult(ShotResultType.Shot, cursor);
        }
    }
}