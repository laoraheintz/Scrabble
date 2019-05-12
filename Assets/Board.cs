using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Board : MonoBehaviour
{
    public struct Box
    {
        public int Line;
        public int Column;
        public Letter Letter; // Null if box empty
        public BoxType BoxType; // Become None if box not empty
    }

    public enum BoxType
    {
        None,
        LetterDouble,
        LetterTriple,
        WordDouble,
        WordTriple
    }

    public Box[,] board = new Box[15, 15];
    
    public bool CheckWord(Box[] word, Box boxUsed)
    {
        // Check that all boxes are available and if not it is the used box
        if (word.Any(box => !CheckBoxAvailability(box) && !box.Equals(boxUsed)))
            return false;
        
        // Check that boxUsed is in the word
        if (!word.Contains(boxUsed))
            return false;
        
        // Check that the letters are aligned (horizontally or vertically)
        var isHorizontal = false;
        if (word.Any(box => box.Line != boxUsed.Line))
        {
            if (word.Any(box => box.Column != boxUsed.Column))
                return false;
        }
        else 
            isHorizontal = true;

        // Check that the letters follow each other
        for (var i = 0; i < word.Length-1; i++)
        {
            if (isHorizontal)
            {
                if (word[i].Line != word[i + 1].Line) 
                    return false;
            }
            else
            {
                if (word[i].Column != word[i + 1].Column)
                    return false;
            }
        }

        return true;
    }

    private static bool CheckBoxAvailability(Box box)
    {
        return box.Letter == null;
    }
    
    public int GetScore(Dictionary<Box, Letter> word)
    {
        var d = 0;
        var t = 0;

        var score = 0;
        
        foreach (var pair in word)
        {
            score += pair.Value.Score;
            switch (pair.Key.BoxType)
            {
                case BoxType.None:
                    break;
                case BoxType.LetterDouble:
                    score += pair.Value.Score;
                    break;
                case BoxType.LetterTriple:
                    score += pair.Value.Score * 2;
                    break;
                case BoxType.WordDouble:
                    d += 1;
                    break;
                case BoxType.WordTriple:
                    t += 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }            
        }

        score *= (int) Mathf.Pow(2, d);
        score *= (int) Mathf.Pow(3, t);

        return score;
    }
    
    public void PutWord(Dictionary<Box, Letter> word)
    {
        foreach (var pair in word)
        {
            PutLetter(pair.Value, pair.Key.Line, pair.Key.Column);
        }
    }

    private void PutLetter(Letter letter, int i, int j)
    {
        board[i, j].Letter = letter;
        board[i, j].BoxType = BoxType.None;
    }
}
