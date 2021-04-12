# Entities

A library that allows to query components efficiently.

In terms of this library, an entity is an object consisting of components.  
The main functionality is contained in the `CachedEntity` class: it is a component implementing the `IEntity` interface. In contrast to Unity's native `GetComponent<T>` (and other similar) functions, it relies on caching. Therefore, the access to components is much faster.

## Installation

1. In Unity, open Window/Package Manager
2. Click the "+" sign and choose "Add package from git URL..."
3. Insert the URL: https://github.com/Delt06/entities.git?path=Packages/com.deltation.entities
4. Click the "Add" button

## Getting Components

An example of querying components:
```c#
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
```


## Entity Actions

Action are delegate-like object (Strategy pattern) with an entity passed as an argument.

An example of action definition:
```c#
using DELTation.Entities;
using DELTation.Entities.Actions;
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
```

An example of action invocation:
```c#
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
``` 

## Notes
- Developed with Unity 2019 LTS
