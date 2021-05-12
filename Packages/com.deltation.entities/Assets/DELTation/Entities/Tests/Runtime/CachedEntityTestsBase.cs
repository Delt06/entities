using NUnit.Framework;
using UnityEngine;

namespace DELTation.Entities.Tests.Runtime
{
    internal abstract class CachedEntityTestsBase
    {
        protected CachedEntity CachedEntity { get; private set; }

        [SetUp]
        public virtual void SetUp()
        {
            CachedEntity = new GameObject().AddComponent<CachedEntity>();
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (CachedEntity)
                Object.Destroy(CachedEntity.GameObject);
        }
    }
}