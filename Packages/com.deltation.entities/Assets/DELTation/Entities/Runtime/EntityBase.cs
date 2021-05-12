using System.Collections.Generic;
using UnityEngine;

namespace DELTation.Entities
{
    public abstract class EntityBase : MonoBehaviour, IEntity
    {
        public GameObject GameObject => gameObject;

        public abstract bool TryGet<T>(out T component) where T : class;
        public abstract T Get<T>() where T : class;
        public abstract IReadOnlyList<T> GetMany<T>() where T : class;
        public abstract ITagCollection Tags { get; }
    }
}