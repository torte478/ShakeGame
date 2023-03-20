using System;
using Shake.Menu;
using Shake.Utils;
using Shake.Zones;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shake.Player
{
    internal sealed class Gun : MonoBehaviour
    {
        private Camera _camera;
        private InputAction _input;
        
        private bool _isLeft = true;
        private Func<Vector3, Zone, Shot> _shot;

        [SerializeField]
        private Zones.Zones zones;

        public event Action<Shot> Fire;

        public Gun()
        {
            _shot = DoFirstShot;
        }

        void Awake()
        {
            _input = new Controls().Player.Shot;
        }

        void Start()
        {
            _camera = Camera.main;
        }

        void OnEnable()
        {
            _input.Enable();
            _input.performed += DoShot;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.performed -= DoShot;
        }

        private void DoShot(InputAction.CallbackContext context)
        {
            if (Pause.Instance.Paused)
                return;
            
            var cursor = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var zone = zones.ToZone(cursor);
            var shot = _shot(cursor, zone);

            Fire.Call(shot);
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