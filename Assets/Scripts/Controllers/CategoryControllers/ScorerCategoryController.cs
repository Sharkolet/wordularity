public class ScorerCategoryController : CategoryController
{
    public int middleWordsAmount;
    public int[] wordPoints;

    public override int calculatePoints(Round round)
    {
        int points = 0;
        foreach (string word in round.getWords())
        {
            int pointsWon = word.Length < middleWordsAmount ? wordPoints[0] : (word.Length == middleWordsAmount ? wordPoints[1] : wordPoints[2]);
            points += pointsWon;
        }

        return points;
    }
}
