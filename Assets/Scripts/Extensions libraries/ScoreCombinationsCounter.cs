using System.Collections.Generic;
using System.Linq;

public static class ScoreCombinationsCounter
{
    public static int GetPigGameScore(List<int> scores)
    {
        var scoresSum = scores.Sum();

        if (scores.Count == scoresSum)
            return 0;
        return scoresSum;
    }

    public static int GetYahtzeeGameScore(List<int> scores)
    {
        var scoresSum = scores.Sum();
        var count = scores.Count;
        var maxScore = scores.Max();
        var minScore = scores.Min();
		var uniqueScoresCount = scores.Distinct().Count();

		if (count <= 3)
            return scoresSum;

        if (maxScore == minScore)
            return scoresSum * 2 + 50;
        if (uniqueScoresCount == count && maxScore - minScore + 1 == count)
            return scoresSum * 3;
        if (uniqueScoresCount == count - 2)
            return scoresSum * 2;

        return scoresSum;
    }
}
