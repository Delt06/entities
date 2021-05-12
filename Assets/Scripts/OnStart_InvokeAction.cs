using DELTation.Entities;
using DELTation.Entities.Actions;
using UnityEngine;

public class OnStart_InvokeAction : MonoBehaviour
{
	public EntityBase Entity;
	public EntityActionAssetBase Action;

	private void Start()
	{
		Action.Invoke(Entity);
	}
}