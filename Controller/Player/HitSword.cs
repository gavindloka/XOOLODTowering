using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSword : MonoBehaviour
{
    private Animator anim;
    
    private float nextHitTime = 0.2f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time - lastClickedTime >maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextHitTime)
        {
            if (Input.GetMouseButtonDown(0) && PlayerMovement.CombatToggle && !PlayerMovement.IsCanvasActive)
            {
                OnClick();
            }
        }
    }
    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;

        if (noOfClicks==1&&PlayerMovement.CombatToggle && !PlayerMovement.IsCanvasActive)
        {
            anim.Play("Hit1");
        }
        noOfClicks = Mathf.Clamp(noOfClicks,0,3); //walopun click lebih bnyk dri 3, ttp return 3
        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && PlayerMovement.CombatToggle && !PlayerMovement.IsCanvasActive)
        {
            anim.Play("Hit2");
        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && PlayerMovement.CombatToggle && !PlayerMovement.IsCanvasActive)
        {
            anim.Play("Hit3");
        }
    }
}
