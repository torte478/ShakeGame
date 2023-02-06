using System.Collections.Generic;
using System.Linq;
using Shake.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace Shake.Enemies
{
    internal sealed class Factory : MonoBehaviour
    {
        private readonly Dictionary<Kind, ObjectPool<Enemy.Enemy>> _pools = new();

        private Transform _transform;
        
        [SerializeField]
        private Enemy.Enemy simpleMelee;
        
        [SerializeField]
        private Enemy.Enemy simpleRemote;
        
        [SerializeField]
        private Enemy.Enemy heavy;
        
        [SerializeField]
        private Enemy.Enemy fast;

        void Awake()
        {
            _transform = GetComponent<Transform>();
            
            CreatePool(Kind.SimpleMelee, simpleMelee);
            CreatePool(Kind.SimpleRemote, simpleRemote);
            CreatePool(Kind.Heavy, heavy);
            CreatePool(Kind.Fast, fast);
        }

        public Enemy.Enemy Create(Kind kind)
        {
            var key = kind == Kind.Random
                          ? GetRandomKind()
                          : kind;

            return _pools[key].Get();
        }

        private Kind GetRandomKind()
            => _pools
               .Keys.Count
               ._(_ => Random.Range(0, _))
               ._(_pools.Keys.ElementAt);

        private void CreatePool(Kind kind, Enemy.Enemy prefab)
        {
            var pool = new ObjectPool<Enemy.Enemy>(
                createFunc: () => CreateEnemy(prefab, kind),
                actionOnGet: e => e.gameObject.SetActive(true),
                actionOnRelease: e => e.gameObject.SetActive(false),
                actionOnDestroy: e => e.Death -= _pools[kind].Release,
                maxSize: 5);
            
            _pools.Add(kind, pool);
        }

        private Enemy.Enemy CreateEnemy(Enemy.Enemy prefab, Kind kind)
        {
            var enemy = Instantiate(prefab, _transform);
            enemy.transform.position = Consts.Outside;

            var obj = enemy.gameObject;
            obj.SetActive(false);
            obj.layer = prefab.gameObject.layer;

            enemy.Death += _pools[kind].Release;
            return enemy;
        }
    }
}