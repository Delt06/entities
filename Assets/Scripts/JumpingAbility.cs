using DELTation.Entities;
using DELTation.Entities.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu]
public class JumpingAbility : EntityActionAssetBase
{
	public Vector3 Velocity = Vector3.up;
	
	protected override void InvokeFor(IEntity entity)
	{
		entity.Get<Rigidbody>().AddForce(Velocity, ForceMode.VelocityChange);
	}
}