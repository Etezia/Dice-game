using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandartScoresAccordance", menuName = "ScriptableObjects/AxisToScoresAccordance")]
public class AxisToScoreAccordance : ScriptableObject
{
	[SerializeField] public Dictionary<int, Vector3> test = new Dictionary<int, Vector3>();

    public int plusZ;
	public int minusZ;
	public int plusX;
	public int minusX;
	public int plusY;
	public int minusY;
}
