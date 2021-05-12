using System;
using System.Collections.Generic;

namespace DELTation.Entities
{
    public sealed class TagCollection : ITagCollection
    {
        public bool Contains<T>() => _counts.ContainsKey(typeof(T));

        public int GetCount<T>() => _counts.TryGetValue(typeof(T), out var count) ? count : 0;

        public void Add<T>() => AddMany<T>(1);

        public void AddMany<T>(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (count == 0) return;

            var type = typeof(T);
            if (_counts.TryGetValue(type, out var currentCount))
                _counts[type] = currentCount + count;
            else
                _counts[type] = count;
        }

        public void Remove<T>() => RemoveMany<T>(1);

        public void RemoveMany<T>(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (count == 0) return;

            var type = typeof(T);
            if (!_counts.TryGetValue(type, out var currentCount)) return;

            var newCount = currentCount - count;
            if (newCount <= 0)
                _counts.Remove(type);
            else
                _counts[type] = newCount;
        }

        public void RemoveAll<T>() => _counts.Remove(typeof(T));

        public void Clear() => _counts.Clear();

        private readonly IDictionary<Type, int> _counts = new Dictionary<Type, int>();
    }
}