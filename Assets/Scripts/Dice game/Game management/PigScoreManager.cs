using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PigScoreManager : MonoBehaviour
{
    [SerializeField] private AxisToScoreAccordance scoresAccordance;
    [SerializeField] private int winScore;

    [SerializeField] private TMP_Text winScoreText;
	[SerializeField] private TMP_Text currPlayerScoreText;

	private int currPlayerScore;

    public void ResetRoundScore()
    {
		currPlayerScore = 0;
        RefreshScore();
	}
    
    public void GetScoresByRotation(List<Rigidbody> diceRbs)
    {
        foreach (var rb in diceRbs)
        {
            var rbTransform = rb.transform;

            if (DotWithUpVector(rbTransform.up) > 1f - 1e-2)
                currPlayerScore += scoresAccordance.Up;
            else if (DotWithUpVector(-rbTransform.up) > 1f - 1e-2)
				currPlayerScore += scoresAccordance.Down;
			else if (DotWithUpVector(rbTransform.forward) > 1f - 1e-2)
				currPlayerScore += scoresAccordance.Forward;
			else if (DotWithUpVector(-rbTransform.forward) > 1f - 1e-2)
				currPlayerScore += scoresAccordance.Back;
			else if (DotWithUpVector(rbTransform.right) > 1f - 1e-2)
				currPlayerScore += scoresAccordance.Right;
			else if (DotWithUpVector(-rbTransform.right) > 1f - 1e-2)
				currPlayerScore += scoresAccordance.Left;
		}
        RefreshScore();
    }

    private float DotWithUpVector(Vector3 vector) => Vector3.Dot(Vector3.up, vector);

    private void RefreshScore() => currPlayerScoreText.text = currPlayerScore.ToString();
}
