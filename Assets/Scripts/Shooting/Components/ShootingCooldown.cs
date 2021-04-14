using UnityEngine;

namespace Shooting.Components
{
	public class ShootingCooldown : MonoBehaviour
	{
		[Min(0f)] public float Cooldown = 1f;
		public float RemainingTime = 0f;
	}
}