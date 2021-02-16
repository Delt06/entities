using DELTation.Entities.Components;
using DELTation.Entities.ScriptableObjects;
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