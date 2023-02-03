using UnityEngine;

namespace Shake.Zones
{
    internal sealed class View : MonoBehaviour
    {
        public void Init(float width)
        {
            var sprite = GetComponent<Transform>();
            
            var scale = sprite.localScale;
            scale.x = width;
            sprite.localScale = scale;
        }
    }
}