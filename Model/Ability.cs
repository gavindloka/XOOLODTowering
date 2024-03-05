using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour { 



    private static Ability instance;
    public static Ability Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Ability();
            }
            return instance;
        }
    }
    public Ability(string name, string description, int price, int damageMultiplier, int hitCount, int coolDown)
    {
        Name = name;
        Description = description;
        Price = price;
        DamageMultiplier = damageMultiplier;
        HitCount = hitCount;
        CoolDown = coolDown;
    }

    public Ability() { }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int DamageMultiplier { get; set; }
    public int HitCount { get; set; }
    public int CoolDown { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
