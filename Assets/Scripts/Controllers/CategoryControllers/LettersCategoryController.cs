using System.Collections.Generic;

public class LettersCategoryController : WordAmountCategoryController
{
    public int lettersAmount;

    public override int getPoperWordsAmount(List<string> words)
    {
        return words.FindAll(word => (lettersAmount == 5 ? word.Length >= lettersAmount : word.Length == lettersAmount)).Count;
    }
}