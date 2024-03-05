using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;

public class EnemyStats : MonoBehaviour
{

    private static EnemyStats instance;
    public static EnemyStats Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyStats();
            }
            return instance;
        }
    }
    private int minLevel;
    private int maxLevel;
    private int minMaxHealth;
    private int minAttack;
    private int minExp;
    private int minGoldDrop;
    private float healthModifier ;
    private float attackModifier;
    private float expDropModifier;
    private float goldDropModifier;

    public int MinLevel
    {
        get { return minLevel; }
        set { minLevel = value; }
    }

    public int MaxLevel
    {
        get { return maxLevel; }
        set { maxLevel = value; }
    }

    public int MinMaxHealth
    {
        get { return minMaxHealth; }
        set { minMaxHealth = value; }
    }

    public int MinAttack
    {
        get { return minAttack; }
        set { minAttack = value; }
    }

    public int MinExp
    {
        get { return minExp; }
        set { minExp = value; }
    }

    public int MinGoldDrop
    {
        get { return minGoldDrop; }
        set { minGoldDrop = value; }
    }

    public float HealthModifier
    {
        get { return healthModifier; }
        set { healthModifier = value; }
    }

    public float AttackModifier
    {
        get { return attackModifier; }
        set { attackModifier = value; }
    }

    public float ExpDropModifier
    {
        get { return expDropModifier; }
        set { expDropModifier = value; }
    }

    public float GoldDropModifier
    {
        get { return goldDropModifier; }
        set { goldDropModifier = value; }
    }

}
