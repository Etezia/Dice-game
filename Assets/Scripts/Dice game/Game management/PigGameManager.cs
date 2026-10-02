using UnityEngine;
using static VectorExtensions;

public class PigGameManager : MonoBehaviour
{
    [SerializeField] private PigScoreManager scoreManager;

    [SerializeField, Range(2, 4)] private int playerCount = 2;
	[SerializeField, Range(1, 5)] private int diceCount = 2;
	[SerializeField] private GamePlace[] gamePlaces = new GamePlace[4];

	private GamePlace currGamePlace;
	private int currPlayerNum;

	private void Awake()
	{
		for (var i = 0; i < playerCount; i++)
		{
			gamePlaces[i].gameObject.SetActive(true);
			gamePlaces[i].SpawnDice(diceCount);
		}

		currGamePlace = gamePlaces[0];
		currPlayerNum = 0;
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void ThrowDice()
	{
		foreach (var die in currGamePlace.DiceRbs)
		{
			die.AddForce(GenerateVectorWithRandomMagnitude(25, 40), ForceMode.VelocityChange);
			die.AddTorque(GenerateVectorWithRandomMagnitude(40, 95), ForceMode.VelocityChange);
		}
	}

	private void SelectNextPlayer()
	{
		currPlayerNum++;
		currPlayerNum %= playerCount;
		currGamePlace = gamePlaces[currPlayerNum];
	}
}
