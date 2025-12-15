using UnityEngine;

public class TurnManager : MonoBehaviour
{
    enum Turns
    {
        Player,
        Opponent1,
        Opponent2
    }

    Turns currentTurn = Turns.Player;

    public void ChangeTurn()
    {
        currentTurn++;
        
        
    }
}
