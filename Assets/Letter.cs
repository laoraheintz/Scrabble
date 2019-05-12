using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    private string _letter = "";
    public int Score { get; private set; } = -1;

    public void Set(string l, int s)
    {
        _letter = l;
        Score = s;
        
        var textLetterUI = GetComponentInChildren<Text>();
        textLetterUI.text = _letter;
                
        var textScoreUI = textLetterUI.GetComponentInChildren<Text>();
        textScoreUI.text = Score.ToString();
    }
}
