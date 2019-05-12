using System.Collections.Generic;

public interface IStrategy
{
    /// <summary>
    /// Pick a move and return it.
    /// </summary>
    /// <returns></returns>
    Dictionary<Board.Box, Letter> PickWord();
}