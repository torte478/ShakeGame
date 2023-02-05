using System;
using Shake.Utils;
using Shake.Zones;
using UnityEngine;

namespace Shake.Player
{
    internal sealed class Gun : MonoBehaviour
    {
        private Camera _camera;
        
        private bool _isLeft = true;
        private Func<Vector3, Zone, Shot> _shot;

        [SerializeField]
        private Zones.Zones zones;

        public Gun()
        {
            _shot = DoFirstShot;
        }

        void Start()
        {
            _camera = Camera.main;
        }

        public Maybe<Shot> DoShot()
        {
            if (!Input.GetMouseButtonDown(0))
                return Maybe.None<Shot>();

            var cursor = _camera.ScreenToWorldPoint(Input.mousePosition);
            var zone = zones.ToZone(cursor);

            var shot = _shot(cursor, zone);
            return Maybe.Some(shot);
        }

        private Shot DoFirstShot(Vector3 cursor, Zone zone)
        {
            var isLeft = zone is Zone.Left or Zone.Center;
            var shot = Shot.Create(cursor, isLeft);

            _isLeft = !isLeft;
            _shot = DoShot;

            return shot;
        }

        private Shot DoShot(Vector3 cursor, Zone zone)
        {
            var isShot = (zone == Zone.Center)
                         || (zone == Zone.Left && _isLeft) 
                         || (zone == Zone.Right && !_isLeft);
            if (!isShot)
                return Shot.Misfire();

            var result = Shot.Create(cursor, _isLeft); 
            _isLeft = !_isLeft;
            return result;
        }
    }
}