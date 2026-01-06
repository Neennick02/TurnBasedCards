using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Scriptable Objects/CardScriptableObject")]
public class CardScriptableObject : ScriptableObject
{
    public enum Type
    {
        Attack,
        Defend,
        Heal,

    }
    public enum Effect
    {
        None,
        Fire,
        Electric,
        Sleep,
        Poison
    }

    public Type CardType;
    public Effect CardEffect;
    
    public string CardName;
    public string CardDescription;
    public int ManaCost;
}
