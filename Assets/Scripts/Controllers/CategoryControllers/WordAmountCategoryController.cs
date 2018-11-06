using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WordAmountCategoryController : CategoryController
{
    protected readonly Color wordsPendingColor = Color.red;

    public List<int> wordsAmount;
    public List<int> wordsPoints;
    public List<Text> wordsAmountTexts;

    public abstract int getPoperWordsAmount(List<string> words);

    public override void updateCategory(Round round, bool isAlreadyWon)
    {
        base.updateCategory(round, isAlreadyWon);
        int properWordsAmount = getPoperWordsAmount(round.getWords());
        if (!isAlreadyWon && properWordsAmount >= wordsAmount[0])
        {
            updateWordsAmountUI(properWordsAmount);
        }
    }

    public void updateWordsAmountUI(int properWordsAmount)
    {
        int index = 0;
        while (index < wordsAmount.Count && properWordsAmount >= wordsAmount[index])
        {
            wordsAmountTexts[index].color = wordsPendingColor;
            index++;
        }
    }

    public override int calculatePoints(Round round)
    {
        int properWordsAmount = getPoperWordsAmount(round.getWords());
        if (properWordsAmount < wordsAmount[0])
        {
            return 0;
        }

        int index = 0;
        while (index < wordsAmount.Count - 1 && properWordsAmount > wordsAmount[index])
        {
            index++;
        }
        return wordsPoints[index];
    }

    public override void resetUI()
    {
        base.resetUI();
        foreach (Text text in wordsAmountTexts)
        {
            text.color = defaultColor;
        }
    }
}
