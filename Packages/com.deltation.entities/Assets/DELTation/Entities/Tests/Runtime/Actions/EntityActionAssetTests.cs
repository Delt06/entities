using NUnit.Framework;
using UnityEngine;

namespace DELTation.Entities.Tests.Runtime.Actions
{
	[TestFixture]
	internal class EntityActionAssetTests : CachedEntityTestsBase
	{
		private CounterEntityActionAsset _action;

		public override void SetUp()
		{
			base.SetUp();
			_action = ScriptableObject.CreateInstance<CounterEntityActionAsset>();
		}

		public override void TearDown()
		{
			base.TearDown();

			if (_action)
				Object.Destroy(_action);
		}

		[Test]
		public void Action_OnNullEntity_ThrowsArgumentNullException()
		{
			Assert.That(() => _action.Invoke(null), Throws.ArgumentNullException);
		}

		[Test]
		public void Action_OnEntity_ExecutesOneTime()
		{
			_action.Invoke(CachedEntity);

			Assert.That(_action.Value, Is.EqualTo(1));
		}
	}
}