using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; set; }
    public int ScorePlayer { get; set; } = 0;

    private int _rackCount = 0;
    private IStrategy _strategy;
    private Board _board;

    private readonly Letter[] _rack = new Letter[7];

    private void Start()
    {
        _strategy = new AI();
        _board = transform.parent.GetComponentInChildren<Board>();
    }

    /// <summary>
    /// Draw letters from the bag. If it's empty and the player has no more letters, the game is over.
    /// </summary>
    /// <returns></returns>
    public bool Draw()
    {
        var draw = GameManager.Instance.PickLetters(_rack.Length - _rackCount).ToList();
        if (draw.Count == 0 && _rackCount == 0)
            return false;

        foreach (var letter in draw)
            _rack[_rackCount++]= letter;
        
        return true;
    }

    /// <summary>
    /// The player play its turn, update its score and allow the next turn.
    /// </summary>
    public void Play()
    { 
        var play = _strategy.PickWord();
        ScorePlayer += Board.GetScore(play);
        _board.PutWord(play); 
        GameManager.Instance.NextTurn();
    }
    
    

    /// <summary>
    /// Update the score of the player if an other player has ended the game.
    /// </summary>
    /// <returns></returns>
    public int UpdateScoreEndGame()
    {
        var points = 0;

        for (var i = 0 ; i < _rackCount ; i++)
            points += _rack[i].Score;
        
        ScorePlayer -= points;
        return points;
    }
}