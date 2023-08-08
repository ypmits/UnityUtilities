using UnityEngine;

namespace Ypmits.Unitytools
{
	public class PhysicsUtils
	{
		/**
		<summary>
		Adds a force to a rigidbody from a particular point with a particular radius
		</summary>
		*/
		public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			Vector2 dir = body.transform.position - explosionPosition;
			float calc = 1 - (dir.magnitude / explosionRadius);
			if (calc <= 0) calc = 0;

			body.AddForce(dir.normalized * explosionForce * calc, ForceMode2D.Impulse);
		}

		/**
		<summary>
		Adds a force to multiple rigidbodies from a particular point with a particular radius
		</summary>
		*/
		public static void AddExplosionForce(Rigidbody2D[] bodies, float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			for (int i = 0; i < bodies.Length; i++) AddExplosionForce(bodies[i], explosionForce, explosionPosition, explosionRadius);
		}
	}
}