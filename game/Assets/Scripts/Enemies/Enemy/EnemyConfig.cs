using UnityEngine;

namespace Shake.Enemies.Enemy
{
    //TODO : rename
    internal sealed class EnemyConfig
    {
        public int Hp { get; }
        
        public float Speed { get; }
        
        public Vector3 Spawn { get; }
        
        public int Attack { get; }
        
        //TODO : rename
        public float AttackDelay { get; }

        public EnemyConfig(int hp, float speed, Vector3 spawn, int attack, float attackDelay)
        {
            Hp = hp;
            Speed = speed;
            Spawn = spawn;
            Attack = attack;
            AttackDelay = attackDelay;
        }
    }
}