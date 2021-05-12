using System;
using JetBrains.Annotations;

namespace DELTation.Entities.Systems.Init
{
    public abstract class InitSystemComponentBase : SystemComponentBase, IInitSystem
    {
        public void Init(IEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            OnInit(entity);
        }

        protected abstract void OnInit([NotNull] IEntity entity);
    }
}