public interface ICategoryController
{
    void updateCategory(Round round, bool isAlreadyWon);
    int calculatePoints(Round round);
}
