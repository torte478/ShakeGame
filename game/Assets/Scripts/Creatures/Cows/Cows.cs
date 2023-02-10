using UnityEngine;

namespace Shake.Creatures.Cows
{
    internal sealed class Cows : Creatures<Cow>
    {
        [SerializeField]
        private Factory factory;

        protected override void ProcessDeath()
        {
            Debug.Log("Game Over! --- Cow");
        }

        protected override Cow SpawnCreature()
            => factory.Create<Cow>();
    }
}