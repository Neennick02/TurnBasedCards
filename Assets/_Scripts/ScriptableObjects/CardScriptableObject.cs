using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Scriptable Objects/CardScriptableObject")]
public class CardScriptableObject : ScriptableObject
{
    public enum Type
    {
        Damage,
        Heal,
        Defend,
        None
    }


    public string cardName;
    public string description;

    [Header("Images")]
    public Sprite artwork;
    public Sprite cardAppearance;

    [Header("Mana")]
    public int manaCost;

    [Header("Effects")]
    public int attack;
    public int heal;
    public int defend;
    [Header("Turn count")]
    public int turns = 1;
    public Type type1;
    public Type type2;
}
