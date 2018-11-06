using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LettersCalculator : MonoBehaviour
{
    public int startingLettersAmount;
    public Text[] lettersAmountTexts;
    public Text bestStarterText;

    public void updateUI(List<string> words)
    {
        int lettersAmount = startingLettersAmount;
        foreach (Text lettersAmountText in lettersAmountTexts)
        {
            lettersAmountText.text = words.Where(word => (lettersAmount == 7 ? word.Length >= lettersAmount : word.Length == lettersAmount)).Count().ToString();
            lettersAmount++;
        }

        fillStarter(words);
    }

    private void fillStarter(List<string> words)
    {
        if (words == null || words.Count == 0)
        {
            bestStarterText.text = " - ";
            return;
        }

        List<string> wordsCopy = new List<string>(words).OrderBy(x => x).ToList<string>();
        int bestStarter = 1;
        int currentStarter = 1;
        char starterLetter = wordsCopy[0][0];
        for (int i = 0; i < wordsCopy.Count - 1; ++i)
        {
            if (wordsCopy[i + 1][0] != wordsCopy[i][0])
            {
                if (currentStarter > bestStarter)
                {
                    bestStarter = currentStarter;
                    starterLetter = wordsCopy[i][0];
                }
                
                currentStarter = 1;
                continue;
            }
            currentStarter++;
        }

        if (currentStarter > bestStarter)
        {
            bestStarter = currentStarter;
            starterLetter = wordsCopy[wordsCopy.Count-1][0];
        }

        bestStarterText.text = starterLetter + " - " + bestStarter.ToString();
    }

    public void reset()
    {
        foreach (Text lettersAmountText in lettersAmountTexts)
        {
            lettersAmountText.text = "0";
            bestStarterText.text = " - ";
        }
    }
}
