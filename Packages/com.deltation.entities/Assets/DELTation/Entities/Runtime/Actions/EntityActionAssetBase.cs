using System;
using JetBrains.Annotations;
using UnityEngine;

namespace DELTation.Entities.Actions
{
    public abstract class EntityActionAssetBase : ScriptableObject, IEntityAction
    {
        public void Invoke(IEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            InvokeFor(entity);
        }

        protected abstract void InvokeFor([NotNull] IEntity entity);
    }
}