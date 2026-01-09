using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Scriptable Objects/CardScriptableObject")]
public class CardScriptableObject : ScriptableObject
{
    public enum type
    {
        Damage,
        Heal,
        Defend,
    }

    public enum secondType
    {
        None,
        Damage,
        Heal,
        Defend,
    }

    public new string cardName;
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int attack;
    public int heal;
    public int defend;
    public int turns = 1;
}
