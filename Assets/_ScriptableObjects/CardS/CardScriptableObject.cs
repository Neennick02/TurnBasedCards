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


    public new string cardName;
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int attack;
    public int heal;
    public int defend;
    public int turns = 1;
    public Type type1;
    public Type type2;
}
