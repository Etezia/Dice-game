using System;
using UnityEngine;
using System.Collections.Generic;
using static VectorExtensions;

public class GamePlace : MonoBehaviour
{
	[SerializeField] private int id;
	[SerializeField] private GameObject diceSkin;
	[SerializeField] private float cameraRotationAngle;
	[SerializeField] private Transform spawner;
	private List<Rigidbody> diceRbs = new List<Rigidbody>();

	public int Id => id;
	public float CameraRotationAngle => cameraRotationAngle;
	public List<Rigidbody> DiceRbs => diceRbs;

	public void SpawnDice(int diceCount)
	{
		for (var i = 0; i < diceCount; i++)
		{
			var currDice = Instantiate(
				diceSkin, new Vector3(spawner.position.x + i * 3, spawner.position.y, spawner.position.z + i * 3), Quaternion.identity);
			diceRbs.Add(currDice.GetComponent<Rigidbody>());
		}
	}

	public void ThrowDices()
	{
		foreach (var die in DiceRbs)
		{
			die.AddForce(GenerateVectorWithRandomMagnitude(30, 45), ForceMode.VelocityChange);
			die.AddTorque(GenerateVectorWithRandomMagnitude(75, 110), ForceMode.VelocityChange);
		}
	}
}