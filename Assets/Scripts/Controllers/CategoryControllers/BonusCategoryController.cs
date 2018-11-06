public class BonusCategoryController : CategoryController
{
    public int oneWordPoints;
    public int lettersAmount;
    public int playerBonusPoints { get; set; }

    public override int calculatePoints(Round round)
    {
        int result = playerBonusPoints;
        result += round.getWords().FindAll(word => word.Length >= lettersAmount).Count * oneWordPoints;

        return result;
    }
}
