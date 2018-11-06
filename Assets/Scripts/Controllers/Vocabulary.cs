using UnityEngine;
using System.Collections.Generic;
using System;

public class Vocabulary {
	private static readonly string VocabularyPath = "Vocabulary/vocabulary_70k";
	private readonly HashSet<string> vocabularyWords = new HashSet<string>();

	public Vocabulary() {
        TextAsset t = Resources.Load(VocabularyPath) as TextAsset;
        foreach (string word in t.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
        {
            vocabularyWords.Add(word.ToUpper());
        }
    }

	public bool containsWord(string word) {
		return vocabularyWords.Contains(word);
	}
}