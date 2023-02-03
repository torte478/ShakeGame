using UnityEngine;

namespace Shake.Enemies.Enemy
{
    //TODO : rename
    internal sealed class EnemyConfig
    {
        public int Hp { get; }
        
        public float Speed { get; }
        
        public int Attack { get; }
        
        //TODO : rename
        public float AttackDelay { get; }
        
        public float AttackSpeed { get; }
        
        public AttackType AttackType { get; }

        public Vector3 Target { get; }

        public EnemyConfig(int hp, float speed, int attack, float attackDelay, float attackSpeed, AttackType attackType, Vector3 target)
        {
            Hp = hp;
            Speed = speed;
            Attack = attack;
            AttackDelay = attackDelay;
            AttackSpeed = attackSpeed;
            AttackType = attackType;
            Target = target;
        }
    }
}