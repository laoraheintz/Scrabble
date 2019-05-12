using UnityEngine;

public class InstantiateTiles : MonoBehaviour
{
    [SerializeField] private GameObject scrabbleLetterPrefab = null;
    
    [SerializeField] private Alphabet alphabet = null;


    public void Awake()
    {
        foreach (var scrabbleLetter in alphabet.scrabbleLetters)
        {
            var go = new GameObject(scrabbleLetter.letter.ToString());
            go.transform.SetParent(transform);
            go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            for (var i = 0; i < scrabbleLetter.amount; i++)
            {
                var letter = Instantiate(scrabbleLetterPrefab, go.transform);
                letter.GetComponent<Letter>().Set(scrabbleLetter.letter, scrabbleLetter.score);
                letter.gameObject.SetActive(false);
            }
        }
    }
}