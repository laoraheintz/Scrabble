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

    public static Board Instance { get; private set; }

    private readonly Box[,] _board = new Box[15, 15];

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

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


    /// <summary>
    /// Check that boxes of the board contain the character and return them.
    /// </summary>
    /// <param name="usedBoardLetter"></param>
    /// <returns></returns>
    public Box GetBoxLetter(Letter usedBoardLetter)
    {
        return _board.Cast<Box>().Where(box => box.Letter == usedBoardLetter).ToArray()[0];
    }
    
    /// <summary>
    /// Get all the Letters on the board that correspond to the character c
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public Letter[] GetLettersChar(char c)
    {
        return (from Box box in _board where box.Letter.Character == c select box.Letter).ToArray();
    }
    
    /// <summary>
    /// Get an array of the necessary boxes before and after the box b on the horizontal axis
    /// </summary>
    /// <param name="b"></param>
    /// <param name="before"></param>
    /// <param name="after"></param>
    /// <returns></returns>
    public Box[] GetBoxesAroundHorizontal(Box b, int before, int after)
    {
        if (b.Column - before < 0 || b.Column + after > _board.GetLength(0))
        {
            return null;
        }

        var boxes = new Box[before + after + 1];
        var i = 0;
        for (var j = b.Column - before ; j < b.Column + after + 1 ; j++)
        {
            boxes[i++] = _board[b.Line, j];
        }

        return boxes;
    }
    
    /// <summary>
    /// Get an array of the necessary boxes before and after the box b on the vertical axis
    /// </summary>
    /// <param name="b"></param>
    /// <param name="before"></param>
    /// <param name="after"></param>
    /// <returns></returns>
    public Box[] GetBoxesAroundVertical(Box b, int before, int after)
    {
        if (b.Line - before < 0 || b.Line + after > _board.GetLength(1))
        {
            return null;
        }

        var boxes = new Box[before + after + 1];
        var j = 0;
        for (var i = b.Line - before ; i < b.Line + after + 1 ; i++)
        {
            boxes[j++] = _board[i, b.Column];
        }

        return boxes;
    }

}
