using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Scriptable Objects/CardScriptableObject")]
public class CardScriptableObject : ScriptableObject
{
    public new string cardName;
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int attack;
    public int health;
    public int defend;

}
