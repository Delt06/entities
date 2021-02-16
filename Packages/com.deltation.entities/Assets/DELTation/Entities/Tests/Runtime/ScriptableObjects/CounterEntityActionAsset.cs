using DELTation.Entities.ScriptableObjects;

namespace DELTation.Entities.Tests.Runtime.ScriptableObjects
{
	internal class CounterEntityActionAsset : EntityActionAssetBase
	{
		public int Value { get; private set; }
		
		protected override void InvokeFor(IEntity entity)
		{
			Value++;
		}
	}
}