using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _turn = 0;
    
    private List<Player> _players;
    
    private readonly Queue<Letter> _bag = new Queue<Letter>();
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        // Select the first player
        _players = new List<Player>(GetComponentsInChildren<Player>());
        var letters = new List<Letter>(GetComponentsInChildren<Letter>());
        var min = 'Z' + 1;
        for (var i = 0 ; i < _players.Count ; i++)
        {
            var rand = Random.Range(0, letters.Count);
            if (letters[rand].Character < min)
            {
                min = letters[rand].Character;
                _turn = i;
            }
            letters.RemoveAt(rand);
        }
        
        // Prepare the random for each draw
        letters = new List<Letter>(GetComponentsInChildren<Letter>());
        var size = letters.Count;
        for (var i = 0 ; i < size ; i++)
        {
            var rand = Random.Range(0, letters.Count);
            _bag.Enqueue(letters[rand]);
            letters.RemoveAt(rand);
        }
        
        // Each player draw their letters
        foreach (var player in _players)
            player.Draw();
    }
    
    /// <summary>
    /// The player whose turn it is draw, if its rack and the bag are empty the game is over.
    /// Otherwise it's the next player's turn.
    /// </summary>
    public void NextTurn()
    {
        if (_players[_turn].Draw())
        {
            if (++_turn >= _players.Count) _turn = 0;
            _players[_turn].Play();
            return;
        }
        RackEmpty();
    }
    
    /// <summary>
    /// Pick a number of Letter in the bag.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public IEnumerable<Letter> PickLetters(int number)
    {
        var draw = new Letter[number];
        if (_bag.Count < number) draw = new Letter[_bag.Count];
        
        for (var i = 0; i < number; i++)
            draw[i] = _bag.Dequeue();

        return draw;
    }

    /// <summary>
    /// There is no more possibilities for any of the players so the game is ending.
    /// </summary>
    private void NoMorePossibilities()
    {
        GameOver();
    }
    
    /// <summary>
    /// A player has ended the game.
    /// </summary>
    private void RackEmpty()
    {
        foreach (var player in _players)
        {
            if (player != _players[_turn]) player.ScorePlayer += player.UpdateScoreEndGame();
        }
        
        GameOver();
    }
    
    /// <summary>
    /// The game is over
    /// </summary>
    private void GameOver()
    {
        var scoreMax = 0;
        Player winner = null;
        
        foreach (var player in _players)
        {
            if (player.ScorePlayer <= scoreMax) continue;
            
            scoreMax = player.ScorePlayer;
            winner = player;
        }
        
        // TO DO : DISPLAY THE WINNER AND SCORES
        // AND RESTART BUTTON
    }
}
