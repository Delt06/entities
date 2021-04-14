using UnityEngine;

namespace Movement.Components
{
	public class BoundingBox : MonoBehaviour
	{
		public Vector2 Center;
		public Vector2 Size;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(XYtoXZ(Center), XYtoXZ(Size));
		}

		private static Vector3 XYtoXZ(Vector2 vector) => new Vector3(vector.x, 0f, vector.y);
	}
}