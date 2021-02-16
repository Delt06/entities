using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DELTation.Entities.Components
{
	public sealed class CachedEntity : EntityBase
	{
		[SerializeField] private bool _searchInInactiveChildren = false;
		[SerializeField, Tooltip("Whether to perform an additional null check on cached components.")] private bool _removeDestroyedComponents = false;

		public bool SearchInInactiveChildren
		{
			get => _searchInInactiveChildren;
			set => _searchInInactiveChildren = value;
		}

		public bool RemoveDestroyedComponents
		{
			get => _removeDestroyedComponents;
			set => _removeDestroyedComponents = value;
		}

		public override T Get<T>() => Get<T>(true);

		private T Get<T>(bool lookUp) where T : class
		{
			var type = typeof(T);
			if (lookUp && _cache.TryGetValue(type, out var componentObject))
			{
				if (!RemoveDestroyedComponents) return (T) componentObject;
				if (!IsDestroyed(componentObject)) return (T) componentObject;
				
				_cache.Remove(type);
				return Get<T>(false);
			}

			componentObject = GetComponentInChildren<T>(_searchInInactiveChildren);
			if ((Object) componentObject == null)
				throw ComponentNotFoundException(type);
			
			_cache[type] = componentObject;

			return (T) componentObject;
		}

		private static bool IsDestroyed(object obj) => obj is Object unityObj && unityObj == null;

		private Exception ComponentNotFoundException(Type type) =>
			new InvalidOperationException(
				$"There is no component of type {type} in {gameObject} or its children.");

		public override T[] GetMany<T>() => GetMany<T>(true);
		
		private T[] GetMany<T>(bool lookUp) where T : class
		{
			var type = typeof(T);
			if (lookUp && _manyCache.TryGetValue(type, out var componentObjects))
			{
				if (!RemoveDestroyedComponents) return (T[]) componentObjects;
				if (!AtLeastOneIsDestroyed<T>(componentObjects)) return (T[]) componentObjects; 
				return GetMany<T>(false);
			}

			componentObjects = GetComponentsInChildren<T>(_searchInInactiveChildren);
			_manyCache[type] = componentObjects;
			return (T[]) componentObjects;
		}

		private static bool AtLeastOneIsDestroyed<T>(object arrayObj) where T : class
		{
			if (!(arrayObj is T[] array)) return false;

			for (var index = 0; index < array.Length; index++)
			{
				if (IsDestroyed(array[index]))
					return true;
			}

			return false;
		}

		private readonly IDictionary<Type, object> _cache = new Dictionary<Type, object>();
		private readonly IDictionary<Type, object> _manyCache = new Dictionary<Type, object>();
	}
}