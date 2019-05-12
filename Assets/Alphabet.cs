using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Alphabet", menuName = "ScriptableObject/Alphabet")]
public class Alphabet : ScriptableObject
{
  [Serializable]
  public struct ScrabbleLetter {
    public string letter;
    public int score;
    public int amount;
  }
  public ScrabbleLetter[] scrabbleLetters;

}

/*scrabbleLetters:
  - letter: A
    score: 1
    amount: 9
  - letter: B
    score: 3
    amount: 2
  - letter: C
    score: 3
    amount: 2
  - letter: D
    score: 2
    amount: 3
  - letter: E
    score: 1
    amount: 15
  - letter: F
    score: 4
    amount: 2
  - letter: G
    score: 2
    amount: 2
  - letter: H
    score: 4
    amount: 2
  - letter: I
    score: 1
    amount: 8
  - letter: J
    score: 8
    amount: 1
  - letter: K
    score: 10
    amount: 1
  - letter: L
    score: 1
    amount: 5
  - letter: M
    score: 2
    amount: 3
  - letter: N
    score: 1
    amount: 6
  - letter: O
    score: 1
    amount: 6
  - letter: P
    score: 3
    amount: 2
  - letter: Q
    score: 8
    amount: 1
  - letter: R
    score: 1
    amount: 6
  - letter: S
    score: 1
    amount: 6
  - letter: T
    score: 1
    amount: 6
  - letter: U
    score: 1
    amount: 6
  - letter: V
    score: 4
    amount: 2
  - letter: W
    score: 10
    amount: 1
  - letter: X
    score: 10
    amount: 1
  - letter: Y
    score: 10
    amount: 1
  - letter: Z
    score: 10
    amount: 1
  - letter: ' '
    score: 0
    amount: 2
    */