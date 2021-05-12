using DELTation.Entities;
using UnityEngine;

[RequireComponent(typeof(IEntity))]
public class VisualsToggle : MonoBehaviour
{
	public void PlayParticleEffect()
	{
		_entity.Get<ParticleSystem>().Play();
	}
	
	public void EnableAllMeshes()
	{
		foreach (var meshRenderer in _entity.GetMany<MeshRenderer>())
		{
			meshRenderer.enabled = true;
		}
	}

	private void Awake()
	{
		_entity = GetComponent<IEntity>();
	}

	private IEntity _entity;
}