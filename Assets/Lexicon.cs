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

        for (var i = 0; i < 4; i++)
        {
            Lexicons[i] = new LexiconNumber(s[i].Split(new[] {" "}, StringSplitOptions.None));
        } 
    }

    public static bool CheckWord(string word)
    {
        return Lexicons[word.Length - 2].NumberLettersWords.Contains(word);
    }
}
