using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSlash : Ability
{
    public HorizontalSlash() : base(
        "Horizontal Slash",
        "In a swift and precise motion, the wielder executes a powerful horizontal strike, cutting through enemies in a wide arc.",
        4000,
        225,
        4,
        5)
    {
        MovingSpeed = 20;
        MaxDistance = 50;
    }

    public int MovingSpeed { get; set; }
    public int MaxDistance { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
