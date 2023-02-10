using System.Collections.Generic;
using System.Linq;
using Shake.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shake.Creatures.Enemies
{
    internal sealed class Factory : MonoBehaviour
    {
        private Dictionary<Kind, Creatures.Factory> _factories;

        [SerializeField]
        private Creatures.Factory simpleMelee;
        
        [SerializeField]
        private Creatures.Factory simpleRemote;
        
        [SerializeField]
        private Creatures.Factory heavy;
        
        [SerializeField]
        private Creatures.Factory fast;

        void Awake()
        {
            _factories = new Dictionary<Kind, Creatures.Factory>
                         {
                             { Kind.SimpleMelee, simpleMelee },
                             { Kind.SimpleRemote, simpleRemote },
                             { Kind.Fast, fast },
                             { Kind.Heavy, heavy }
                         };
        }

        public Enemy.Enemy Create(Kind kind)
        {
            var key = kind == Kind.Random
                          ? GetRandomKind()
                          : kind;

            return _factories[key].Create<Enemy.Enemy>();
        }

        private Kind GetRandomKind()
            => _factories
               .Keys.Count
               ._(_ => Random.Range((int)0, (int)_))
               ._(_factories.Keys.ElementAt);
    }
}