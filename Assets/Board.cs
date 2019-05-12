using System;
using System.Collections.Generic;
using System.Linq;
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

    private readonly Box[,] _board = new Box[15, 15];

    /// <summary>
    /// Set the box : i is th Line and j the Column.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="boxType"></param>
    public void SetBox(int i, int j, BoxType boxType)
    {
        _board[i, j].Line = i;
        _board[i, j].Column = j;
        _board[i, j].BoxType = boxType;
    }
    
    /// <summary>
    /// Check that a word can be placed there.
    /// </summary>
    /// <param name="word"></param>
    /// <param name="boxUsed"></param>
    /// <returns></returns>
    public bool CheckWordSpace(Box[] word, Box boxUsed)
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

    /// <summary>
    /// Check that a box is empty.
    /// </summary>
    /// <param name="box"></param>
    /// <returns></returns>
    private static bool CheckBoxAvailability(Box box)
    {
        return box.Letter == null;
    }
    
    /// <summary>
    /// Get the score generate by this move.
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static int GetScore(Dictionary<Box, Letter> word)
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

    /// <summary>
    /// Put the word on the board.
    /// </summary>
    /// <param name="word"></param>
    public void PutWord(Dictionary<Box, Letter> word)
    {
        foreach (var pair in word)
        {
            PutLetter(pair.Value, pair.Key.Line, pair.Key.Column);
        }
    }

    /// <summary>
    /// Put a letter on the box line i and column j. The box looses its type if it had one.
    /// </summary>
    /// <param name="letter"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    private void PutLetter(Letter letter, int i, int j)
    {
        _board[i, j].Letter = letter;
        _board[i, j].BoxType = BoxType.None;
    }
}
