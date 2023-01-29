using DG.Tweening;
using UnityEngine;

namespace Shake.Enemies
{
    internal sealed class Enemy : MonoBehaviour
    {
        public State State { get; private set; } = State.Start;

        public void Spawn(Vector3 from, Vector3 to, float speed)
        {
            State = State.Spawn;

            transform.position = from;
            var duration = Vector3.Distance(from, to) * 1000f / speed;
            
            Debug.Log(duration);
            
            transform
                .DOMove(to, duration)
                .OnComplete(OnSpawnComplete);
        }

        private void OnSpawnComplete()
        {
            State = State.Ready;
        }
    }
}