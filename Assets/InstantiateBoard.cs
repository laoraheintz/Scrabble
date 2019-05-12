using System.Linq;
using UnityEngine;

public class InstantiateBoard : MonoBehaviour
{
    [SerializeField] private GameObject letterDoubleBoxPrefab;
    [SerializeField] private GameObject letterTripleBoxPrefab;
    [SerializeField] private GameObject wordDoubleBoxPrefab;
    [SerializeField] private GameObject wordTripleBoxPrefab;

    [SerializeField] private Vector2[] letterDoublePositions = null;
    [SerializeField] private Vector2[] letterTriplePositions = null;
    [SerializeField] private Vector2[] wordDoublePositions = null;
    [SerializeField] private Vector2[] wordTriplePositions = null;

    private Board _board;

    private void Awake()
    {
        _board = gameObject.AddComponent<Board>();

        for (var i = 0; i < 15; i++)
        {
            for (var j = 0; j < 15; j++)
            {
                _board.board[i, j].Line = i;
                _board.board[i, j].Column = j;

                if (letterDoublePositions.Contains(new Vector2(i, j)))
                    _board.board[i, j].BoxType = Board.BoxType.LetterDouble;
                else if (letterTriplePositions.Contains(new Vector2(i, j)))
                    _board.board[i, j].BoxType = Board.BoxType.LetterTriple;
                else if (wordDoublePositions.Contains(new Vector2(i, j)))
                    _board.board[i, j].BoxType = Board.BoxType.WordDouble;
                else if (wordTriplePositions.Contains(new Vector2(i, j)))
                    _board.board[i, j].BoxType = Board.BoxType.WordTriple;
                else
                    _board.board[i, j].BoxType = Board.BoxType.None;
            }
        }
    }
}

