using System.Collections.Generic;
using System.Linq;

public class AI : IStrategy
{
    public Dictionary<Board.Box, Letter> PickWord(Letter[] letters)
    {
        var lexicons = Lexicon.Lexicons;
        // Go through all lexicons by word length, beginning with the longest words
        for (var i = lexicons.Length ; i > 0 ; i--) 
        {
            // Go through all words of this lexicon
            foreach (var word in lexicons[i].NumberLettersWords)
            {
                var rack = letters.ToList();
                var usedLetters = new List<Letter>();
                var usedBoardCharPlace = -1;

                // Go through all character of the word
                for (var index = 0; index < word.Length; index++)
                {
                    var c = word[index];
                    var l = rack.Find(letter => letter.Character == c);
                    if (l != null)
                    {
                        usedLetters.Add(l);
                        rack.Remove(l);
                    }
                    else if (usedBoardCharPlace == -1)
                    {
                        var lettersBoard = Board.Instance.GetLettersChar(c);
                        if (lettersBoard.Length > 0)
                        {
                            usedLetters.Add(lettersBoard[0]);
                            usedBoardCharPlace = index;
                        }
                        else break;
                    }
                    else
                    {
                        usedBoardCharPlace = -1;
                        break;
                    }

                    // If we have all the necessary characters
                    if (usedLetters.Count == word.Length) break;
                }

                // If we don't use or more than one board character, next word
                if (usedBoardCharPlace == -1) continue;
                
                var move = CheckPlace(usedLetters, usedBoardCharPlace, Board.Instance.GetBoxLetter(usedLetters[usedBoardCharPlace]));

                // If there is no place for the word, next word
                if (move == null) continue;

                return move;
            }
        }
        return null;
    }

    /// <summary>
    /// Find the place for the word and return it.
    /// </summary>
    /// <param name="usedChars"></param>
    /// <param name="usedBoardCharPlace"></param>
    /// <param name="box"></param>
    /// <returns></returns>
    private static Dictionary<Board.Box, Letter> CheckPlace(List<Letter> usedChars, int usedBoardCharPlace, Board.Box box)
    {
        var horizontalBoxes = Board.Instance.GetBoxesAroundHorizontal(box, usedBoardCharPlace, usedChars.Count - usedBoardCharPlace - 1);
        if (horizontalBoxes != null)
        {
            if (Board.Instance.CheckWordSpace(horizontalBoxes, box))
            {
                return horizontalBoxes.Zip(usedChars, (k, v) => new { k, v }) .ToDictionary(x => x.k, x => x.v);

            }
        }
        
        var verticalBoxes = Board.Instance.GetBoxesAroundVertical(box, usedBoardCharPlace, usedChars.Count - usedBoardCharPlace - 1);
        if (verticalBoxes != null)
        {
            if (Board.Instance.CheckWordSpace(verticalBoxes, box))
            {
                return verticalBoxes.Zip(usedChars, (k, v) => new { k, v }) .ToDictionary(x => x.k, x => x.v);
            }
        }

        return null;
    }
}
