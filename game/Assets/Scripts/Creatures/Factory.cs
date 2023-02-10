using System;
using Shake.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace Shake.Creatures
{
    internal sealed class Factory : MonoBehaviour
    {
        private ObjectPool<Creature> _pool;
        private Transform _transform;

        [SerializeField]
        private Creature prefab;

        void Awake()
        {
            _transform = GetComponent<Transform>();
            
            _pool = new ObjectPool<Creature>(
                createFunc: CreateCreature,
                actionOnGet: e => e.gameObject.SetActive(true),
                actionOnRelease: e => e.gameObject.SetActive(false),
                actionOnDestroy: e => e.Death -= _pool.Release,
                maxSize: 5);
        }

        public T Create<T>()
            where T : Creature
        {
            var creature = _pool.Get();
            if (creature is not T casted)
                throw new Exception($"Expected type {typeof(T).FullName}, but actual {creature.GetType().FullName}");

            return casted;
        }

        private Creature CreateCreature()
        {
            var creature = Instantiate(prefab, _transform);
            creature.transform.position = Consts.Outside;

            var owner = creature.gameObject;
            owner.SetActive(false);
            owner.layer = prefab.gameObject.layer;

            creature.Death += _pool.Release;
            return creature;
        }
    }
}