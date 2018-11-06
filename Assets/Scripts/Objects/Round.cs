using System;
using System.Collections.Generic;

public class Round {
    private readonly char[] VOWELS = { 'A', 'E', 'I', 'O', 'U'};
    private readonly char[] CONSONANTS = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z'};
    private readonly char[] ALPHABET = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

    private readonly int minVowelsAmount = 3;
    private readonly int maxVowelsAmount = 4;
    private readonly int lettersAmount = 10;

    public RoundWinType winType { get; set; }
	private List<string> words;
	private List<char> letters;

	public Round()
    {
        this.words = new List<string>(10);
        this.letters = new List<char>();

        Random rand = new Random();
        int vowelsAmount = minVowelsAmount + rand.Next(maxVowelsAmount - minVowelsAmount + 1);
        for (int i = 0; i < vowelsAmount; ++i)
        {
            letters.Add(VOWELS[rand.Next(VOWELS.Length)]);
        }
        for (int i = 0; i < lettersAmount - vowelsAmount; ++i)
        {
            letters.Add(CONSONANTS[rand.Next(CONSONANTS.Length)]);
        }
        letters = ShuffleList<char>(letters);
	}

	public void addWord(string word)
    {
		words.Add(word);
	}

	public List<string> getWords()
    {
		return words;
	}

	public List<char> getLetters()
    {
		return letters;
	}

    private List<E> ShuffleList<E>(List<E> inputList)
    {
        List<E> randomList = new List<E>();

        Random r = new Random();
        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = r.Next(0, inputList.Count);
            randomList.Add(inputList[randomIndex]);
            inputList.RemoveAt(randomIndex);
        }

        return randomList;
    }
}
