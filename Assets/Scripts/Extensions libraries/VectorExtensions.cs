using UnityEngine;

public static class VectorExtensions
{
	public static Vector3 GenerateVectorWithRandomMagnitude(float minMagnitude, float maxMagnitude)
	{
		var randomVector = Vector3.up * Random.Range(minMagnitude, maxMagnitude);
		var randomAngle = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

		return randomAngle * randomVector;
	}
}
