using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int MaxHealth;
    public int Health;
    public int CurrentMana;
    public int MaxMana;
    public int BaseManaAmount;

    public int Defence;

}
