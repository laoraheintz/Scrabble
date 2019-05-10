using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string _name;
    private readonly Letter[] _rack = new Letter[7];
    public int rackCount = 0;
    
    public void Draw(IEnumerable<Letter> draw)
    {
        foreach (var letter in draw)
            _rack[rackCount++]= letter;
    }
}
