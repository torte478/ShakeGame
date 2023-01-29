using UnityEngine;

namespace Shake.Enemies.Enemy
{
    //TODO : rename
    internal sealed class EnemyConfig
    {
        public int Hp { get; }
        
        public float Speed { get; }
        
        public Vector3 Spawn { get; }

        public EnemyConfig(int hp, float speed, Vector3 spawn)
        {
            Hp = hp;
            Speed = speed;
            Spawn = spawn;
        }
    }
}