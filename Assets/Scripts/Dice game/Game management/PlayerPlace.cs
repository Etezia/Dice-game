using System;
using UnityEngine;
using System.Collections.Generic;
using static VectorExtensions;

public class PlayerPlace : MonoBehaviour
{
	[SerializeField] private GameObject diceSkin;
	[SerializeField] private Transform spawner;

	[field: SerializeField] public string PlayerName { get; private set; }
	[field: SerializeField] public float CameraRotationAngle { get; private set; }
	public List<Rigidbody> DiceRbs { get; } = new List<Rigidbody>();

	public int OverallScore { get; set; }

	public void SpawnDice(int diceCount)
	{
		for (var i = 0; i < diceCount; i++)
		{
			var currDice = Instantiate(
				diceSkin, new Vector3(spawner.position.x + i * 3, spawner.position.y, spawner.position.z + i * 3), Quaternion.identity);
			DiceRbs.Add(currDice.GetComponent<Rigidbody>());
		}
	}

	public void ThrowDice()
	{
		foreach (var die in DiceRbs)
		{
			die.AddForce(GenerateVectorWithRandomMagnitude(25, 40), ForceMode.VelocityChange);
			die.AddTorque(GenerateVectorWithRandomMagnitude(75, 110), ForceMode.VelocityChange);
		}
	}
}