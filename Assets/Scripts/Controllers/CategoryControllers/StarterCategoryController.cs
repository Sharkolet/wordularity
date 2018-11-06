using System;
using System.Collections.Generic;
using System.Linq;

public class StarterCategoryController : WordAmountCategoryController
{
    public override int getPoperWordsAmount(List<string> words)
    {
        if (words.Count == 0)
        {
            return 0;
        }

        List<string> wordsCopy = new List<string>(words).OrderBy(x => x).ToList<string>();
        int longestStreak = 1;
        int currentStreak = 1;
        for (int i = 0; i < wordsCopy.Count - 1; ++i)
        {
            if (wordsCopy[i + 1][0] != wordsCopy[i][0])
            {
                longestStreak = Math.Max(currentStreak, longestStreak);
                currentStreak = 1;
                continue;
            }
            currentStreak++;
        }

        longestStreak = Math.Max(currentStreak, longestStreak);

        return longestStreak;
    }
}
