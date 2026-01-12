using System;
using UnityEngine;

public class OverTimeEffect : MonoBehaviour
{
    public int EffectAmountPerTurn;
    public int remainingTurns;
    public EffectType Type;
    public enum EffectType
    {
        Heal,
        Attack,
        Defend
    }
    public OverTimeEffect(int damagePerTurn, int turns, EffectType type)
    {
        this.EffectAmountPerTurn = damagePerTurn;
        this.remainingTurns = turns;
        this.Type = type;
    }
}
