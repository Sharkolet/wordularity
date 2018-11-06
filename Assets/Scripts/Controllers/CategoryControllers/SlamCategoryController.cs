using System.Collections.Generic;
using System.Linq;

public class SlamCategoryController : CategoryController
{
    public int categoryPoints;

    public override int calculatePoints(Round round)
    {
        foreach (char letter in round.getLetters())
        {
            List<string> wordsFromLetter = round.getWords().Where(word => word[0].Equals(letter.ToString())).ToList();
            if (wordsFromLetter == null || wordsFromLetter.Count() == 0)
            {
                return 0;
            }
        }

        return categoryPoints;
    }
}
