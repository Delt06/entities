using System.Collections.Generic;
using UnityEngine;

namespace DELTation.Entities
{
	public interface IEntity
	{
		GameObject GameObject { get; }
		bool TryGet<T>(out T component) where T : class;
		T Get<T>() where T : class;
		IReadOnlyList<T> GetMany<T>() where T : class;

		ITagCollection Tags { get; }
	}
}