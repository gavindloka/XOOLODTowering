using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private static PlayerStats instance;
    
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerStats();
            }
            return instance;
        }
    }

    protected int level = 1;
    protected int maxHealth = 1000;
    protected float healthIncreaseModifier = 0.1f;
    protected int attack = 50;
    protected float attackIncreaseModifier = 0.1f;
    protected int constantExp = 500;
    protected float expIncreaseModifier = 0.25f;
    protected float healthRegenCooldown = 3f;
    protected int gold = 100000;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float HealthIncreaseModifier
    {
        get { return healthIncreaseModifier; }
        set { healthIncreaseModifier = value; }
    }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public float AttackIncreaseModifier
    {
        get { return attackIncreaseModifier; }
        set { attackIncreaseModifier = value; }
    }

    public int ConstantExp
    {
        get { return constantExp; }
        set { constantExp = value; }
    }

    public float ExpIncreaseModifier
    {
        get { return expIncreaseModifier; }
        set { expIncreaseModifier = value; }
    }

    public float HealthRegenCooldown
    {
        get { return healthRegenCooldown; }
        set { healthRegenCooldown = value; }
    }

    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

}
