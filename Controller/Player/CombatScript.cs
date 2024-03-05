using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    public GameObject ThighSword;
    public GameObject HandSword;
    public GameObject HitEffect;
    private PlayerStats ps;

    void Start()
    {
        ps = PlayerStats.Instance;
    }
    public void DrawSword()
    {
        ThighSword.SetActive(false); 
        HandSword.SetActive(true);

    }

    public void SheathSword()
    {
        ThighSword.SetActive(true);
        HandSword.SetActive(false);
    }

    public void DamageEnabled()
    {
        Sword.DamageAmount = ps.Attack;
        StartCoroutine(GiveHitEffect());
    }
    public void DamageDisabled()
    {
        Sword.DamageAmount = 0;
    }
    IEnumerator GiveHitEffect()
    {
        HitEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        HitEffect.SetActive(false);
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
