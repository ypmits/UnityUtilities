using UnityEngine;


public class SimpleMath
{
    /**
    <summary>
    Return whichever number has a lower absolute value.
    </summary>
    */
    public static float LowestAbs(float a, float b) => Mathf.Abs(a) < Mathf.Abs(b) ? a : b;

    /**
    <summary>
    Return a point on a circle by giving an angle and a distance (radius)
    </summary>
    <returns>The point from angle and radius.</returns>
    <param name="angle">Angle.</param>
    <param name="radius">Radius of the circle.</param>
    */
    public static Vector2 GetPointOnCircle(float angle, float radius) => new Vector2(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad));

    /**
    <summary>
    Sets an (euler)-angle between two points
    </summary>
    */
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
    public static float GetAmountByPercentage(float min, float max, float percentage) => ((max < min) ? (min - max) : (min + max)) * percentage;

    /**
    <summary>
    Returns an angle (in degrees) from a vector
    </summary>
    */
    public static float GetAngleFromVector(Vector2 vec) => Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;

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