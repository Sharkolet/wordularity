using System;
using System.Collections.Generic;
using System.Linq;

public class StraightCategoryController : WordAmountCategoryController
{
    public override int getPoperWordsAmount(List<string> words)
    {
        List<string> wordsCopy = new List<string>(words).OrderBy(x => x.Length).ToList<string>();
        int longestStreak = 1;
        int currentStreak = 1;
        for (int i = 0; i < wordsCopy.Count - 1; ++i)
        {
            int lettersDifference = wordsCopy[i + 1].Length - wordsCopy[i].Length;
            if (lettersDifference > 1)
            {
                longestStreak = Math.Max(currentStreak, longestStreak);
                currentStreak = 1;
                continue;
            } else if (lettersDifference == 1)
            {
                currentStreak++;
            }
        }

        return Math.Max(currentStreak, longestStreak);
    }
}
