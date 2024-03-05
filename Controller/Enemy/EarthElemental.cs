using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EarthElemental : EnemyStats
{
    public int Level;

    public Animator Animator;
    public Slider HealthBar;
    public GameObject damageText;
    public EnemySpawner Spawner;
    public TextMeshProUGUI LevelText;
    private int LevelDifference;
    PlayerStats ps;
    public static int minAttack;
    private void Start()
    {
        Spawner = GameObject.FindGameObjectWithTag("EarthElementalSpawner").GetComponent<EnemySpawner>();
        ps = PlayerStats.Instance;
        MinLevel = 35;
        MaxLevel = 45;
        MinMaxHealth = 200; //ganti jadi 4000
        MinAttack = 650;
        MinExp = 1000;
        MinGoldDrop = 300;
        HealthModifier = 0.25f;
        AttackModifier = 0.1f;
        ExpDropModifier = 0.2f;
        GoldDropModifier = 0.2f;
        Level = Random.Range(MinLevel,MaxLevel+1);
        HealthBar.maxValue = MinMaxHealth;
        minAttack = MinAttack;

    }
    public void Update()
    {
        HealthBar.value = MinMaxHealth;
        LevelText.text = "Lv. " + Level.ToString();
        LevelDifference = ps.Level - Level;
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
        if (MinMaxHealth <= 0)
        {
            StartCoroutine(DestroyAfterDeath());
        }
        else if (damageAmount > 0)
        {
            //play gethit animation
            Animator.SetTrigger("getDamage");
            DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
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
