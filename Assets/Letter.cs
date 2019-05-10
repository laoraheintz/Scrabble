using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    private string _letter;
    private int _score;

    public void Set(string l, int s)
    {
        _letter = l;
        _score = s;
        
        var textLetterUI = GetComponentInChildren<Text>();
        textLetterUI.text = _letter;
                
        var textScoreUI = textLetterUI.GetComponentInChildren<Text>();
        textScoreUI.text = _score.ToString();
    }
}
