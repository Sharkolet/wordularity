using System.Collections.Generic;

public class YachtCategoryController : WordAmountCategoryController
{
    public override int getPoperWordsAmount(List<string> words)
    {
        return words.Count;
    }
}
