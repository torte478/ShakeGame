using UnityEngine;

namespace Player
{
    internal sealed class GunComponent
    {
        private bool _isLeft = true;
        
        public ShotResult TryShot()
        {
            if (!Input.GetMouseButtonDown(0))
                return new ShotResult(ShotResultType.None);

            var cursor = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(cursor);
            
            return new ShotResult(ShotResultType.None);
        }
    }
}