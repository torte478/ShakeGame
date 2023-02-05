using System;

namespace Shake.Enemies.Enemy.Attack
{
    internal interface IAttack
    {
        event Action Finish;
        
        void Start();
    }
}