using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    public char Character { get; private set; } = ' ';
    public int Score { get; private set; } = -1;

    public void Set(char l, int s)
    {
        Character = l;
        Score = s;
        
        var textLetterUI = GetComponentInChildren<Text>();
        textLetterUI.text = Character.ToString();
                
        var textScoreUI = textLetterUI.GetComponentInChildren<Text>();
        textScoreUI.text = Score.ToString();
    }
}
