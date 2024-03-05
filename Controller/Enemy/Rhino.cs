using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rhino : EnemyStats
{
    private int Level;
    public Animator Animator;
    public Slider HealthBar;
    public GameObject damageText;
    public EnemySpawner Spawner;
    public TextMeshProUGUI LevelText;
    private int LevelDifference;

    public int level;
    private PlayerStats ps;

    private void Start()
    {
        Spawner = GameObject.FindGameObjectWithTag("RhinoSpawner").GetComponent<EnemySpawner>();
        ps = PlayerStats.Instance;
        MinLevel = 1;
        MaxLevel = 20;
        MinMaxHealth = 100; //ganti jadi 1000
        MinAttack = 10; //ganti jadi 100
        MinExp = 100;
        MinGoldDrop = 100;
        HealthModifier = 0.1f;
        AttackModifier = 0.1f;
        ExpDropModifier = 0.1f;
        GoldDropModifier = 0.1f;
        Level = Random.Range(MinLevel,MaxLevel+1);
        HealthBar.maxValue = MinMaxHealth;
    }
    public void Update()
    {
        HealthBar.value = MinMaxHealth;
        LevelText.text = "Lv. "+ Level.ToString();
        LevelDifference = ps.Level-Level;
        if (LevelDifference >= 4)
        {
            LevelText.color = Color.white;
        }
        else if (LevelDifference >= 3)
        {
            LevelText.color = Color.green;
        }
        else if (LevelDifference <= -10) 
        {
            LevelText.color = Color.red;
        }
        else if (LevelDifference <= -4)
        {
            LevelText.color = Color.yellow;
        }
    }
    public void TakeDamage(int damageAmount)
    {
        MinMaxHealth -= damageAmount;
        if (MinMaxHealth<=0)
        {
            StartCoroutine(DestroyAfterDeath());
            
        }
        else if(damageAmount > 0)
        {
            //play gethit animation
            Animator.SetTrigger("getDamage");
            DamageIndicator indicator = Instantiate(damageText,transform.position,Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damageAmount);
        }
    }
    IEnumerator DestroyAfterDeath()
    {
        Animator.SetTrigger("dead");
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        Spawner.DecreaseEnemyCount();
    }
}
