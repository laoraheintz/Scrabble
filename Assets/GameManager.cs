using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly Queue<Letter> _bag = new Queue<Letter>();
    
    private readonly Player[] _players = new Player[2];

    private void Start()
    {
        _players[0] = new GameObject().AddComponent<Player>();
        _players[1] = new GameObject().AddComponent<Player>();

        var letters = new List<Letter>(FindObjectsOfType<Letter>());
        var size = letters.Count;
        for (var i = 0 ; i < size ; i++)
        {
            var rand = Random.Range(0, letters.Count);
            _bag.Enqueue(letters[rand]);
            letters.RemoveAt(rand);
        }
        
        _players[0].Draw(PickLetters(7));
        _players[1].Draw(PickLetters(7));
    }

    private IEnumerable<Letter> PickLetters(int number)
    {
        var draw = new Letter[number];

        for (var i = 0; i < number; i++)
            draw[i] = _bag.Dequeue();
        
        return draw;
    }
}
