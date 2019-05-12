using UnityEngine;

public class InstantiatePlayers : MonoBehaviour
{
    [SerializeField] private int playerNumber = 2;

    private void Awake()
    {
        for (var i = 0 ; i < playerNumber ; i++)
        {
            var player = new GameObject().AddComponent<Player>();
            player.transform.SetParent(transform);
            
            var n = i + 1;
            player.name = "Player " + n;
            player.Name = "Player " + n;
        }
    }
}