using UnityEngine;

namespace Movement.Components
{
	public class Speed : MonoBehaviour
	{
		[Min(0f)] public float Value = 1f;
	}
}