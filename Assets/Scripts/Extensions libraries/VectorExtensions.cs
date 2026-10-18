using UnityEngine;

public static class VectorExtensions
{
	public static Vector3 GenerateVectorWithRandomMagnitude(float minMagnitude, float maxMagnitude)
	{
		var randomVector = Vector3.up * Random.Range(minMagnitude, maxMagnitude);
		var randomAngle = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 360), 0f);

		return randomAngle * randomVector;
	}

	public static float DotWithUpVector(Vector3 vector) => Vector3.Dot(Vector3.up, vector);
}
