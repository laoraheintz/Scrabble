using System;
using System.Linq;
using UnityEngine;

public class Lexicon : MonoBehaviour
{
    private struct LexiconNumber
    {
        public readonly string[] NumberLettersWords;

        public LexiconNumber(string[] numberLettersWords)
        {
            NumberLettersWords = numberLettersWords;
        }
    }
    
    private static readonly LexiconNumber[] Lexicons = new LexiconNumber[4];

    private void Start()
    {
        var lexicon = Resources.Load<TextAsset>("Lexicon");
        var s = lexicon.text.Split(new[] { "\n" }, StringSplitOptions.None);

        for (var i = 0; i < s.Length; i++)
        {
            Lexicons[i] = new LexiconNumber(s[i].Split(new[] {" "}, StringSplitOptions.None));
        } 
    }

    /// <summary>
    /// Check if the word is present in the Scrabble Dictionnary.
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public static bool CheckWord(string word)
    {
        return Lexicons[word.Length - 2].NumberLettersWords.Any(word.Contains);
    }
}