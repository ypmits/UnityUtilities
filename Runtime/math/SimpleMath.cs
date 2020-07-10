using UnityEngine;


namespace nl.ypmits.gametools
{
	public class SimpleMath
	{
		public static bool SetAngleBetweenTwoPoints(Transform transf, Vector3 p1, Vector3 p2, bool useLerp = true, float degreeCorrection = 0, float minAngle = -180, float maxAngle = 180)
		{
			var dir = p1 - p2;
			var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + degreeCorrection;
			var newRotation = Quaternion.AngleAxis(Mathf.Clamp(angle, minAngle, maxAngle), Vector3.forward);
			transf.rotation = useLerp ? Quaternion.Lerp(transf.rotation, newRotation, Time.deltaTime * 4) : newRotation;
			return !(angle < minAngle || angle > maxAngle);
		}

		/**
		<summary>
		GetPercentageOf( 255, 0, 0.1f ); // returns 127.5
		</summary>
		*/
		public static float GetAmountByPercentage(float min, float max, float percentage)
		{
			return ((max < min) ? (min - max) : (min + max)) * percentage;
		}

		public static float GetAngleFromVector(Vector2 vec)
		{
			return Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;
		}

		/**
		<summary>
		Returns a random true or false based on the given probabilityPercentage (default at 50%)
		</summary>
		*/
		public static bool GetRandomBool(int probabilityPercentage = 50)
		{
			System.Random gen = new System.Random();
			int prob = gen.Next(100);
			return prob <= probabilityPercentage;
		}
	}
}