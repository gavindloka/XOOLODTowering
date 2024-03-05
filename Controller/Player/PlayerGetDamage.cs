using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerGetDamage : MonoBehaviour
{
    PlayerStats ps;


    //public static int Level = 1;
    private int HP;
    public int minHP = 0;
    public Animator Animator;
    public GameObject damageText;
    public TextMeshProUGUI Text;
    public static bool IsDead;
    public Slider HealthSlider;

    private bool isRegenerating = true;
    private float regenerationDelay = 8f;
    private float regenerationRate = 1f;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        ps = PlayerStats.Instance;
        Animator = GetComponent<Animator>();
        StartCoroutine(HealthRegen());
        HP = ps.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = HP.ToString();
        if (HP <= 0)
        {
            HP = minHP;
        }
        HealthSlider.value = HP;
    }

    IEnumerator HealthRegen()
    {
        while (true)
        {
            if (isRegenerating)
            {
                float regenAmount = ps.MaxHealth * regenerationRate / 100f;
                HP = Mathf.Min(ps.MaxHealth, HP + Mathf.RoundToInt(regenAmount));
            }
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator DisableRegeneration()
    {
        yield return new WaitForSeconds(regenerationDelay);
        isRegenerating = true;
    }
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            StartCoroutine(Respawn());
            
        }else if (damageAmount>0)
        {
            DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damageAmount);
        }

        if (isRegenerating)
        {
            isRegenerating = false;
            StartCoroutine(DisableRegeneration());
        }
    }
    IEnumerator Respawn()
    {
        Animator.SetTrigger("dead");
        IsDead = true;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainScene");
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            IsDead = false;
        }
    }
}
