using System;
using UnityEngine;

namespace DELTation.Entities.Systems
{
    public abstract class SystemComponentBase : MonoBehaviour, ISystem
    {
        public virtual bool ShouldBeExecuted(IEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return _isEnabled;
        }

        protected void OnEnable()
        {
            _isEnabled = true;
            OnEnabled();
        }

        protected virtual void OnEnabled() { }

        protected void OnDisable()
        {
            _isEnabled = false;
            OnDisabled();
        }

        protected virtual void OnDisabled() { }

        private bool _isEnabled;
    }
}