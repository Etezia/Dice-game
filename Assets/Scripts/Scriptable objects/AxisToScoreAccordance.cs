using UnityEngine;


[CreateAssetMenu(fileName = "StandartScoresAccordance", menuName = "ScriptableObjects/AxisToScoresAccordance")]
public class AxisToScoreAccordance : ScriptableObject
{
    public int Forward;
	public int Back;
	public int Right;
	public int Left;
	public int Up;
	public int Down;
}
