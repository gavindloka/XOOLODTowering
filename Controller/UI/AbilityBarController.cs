using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBarController : MonoBehaviour
{
    public Image Ability1;
    public Image Ability2;
    public Image Ability3;
    public Image Ability4;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        DisplayBoughtAbilities();
    }
    void DisplayBoughtAbilities() {
        List<Image> boughtImages = BuyAbilityController.boughtImages;

        if (boughtImages.Count >= 1)
            Ability1.sprite = boughtImages[0].sprite;

        if (boughtImages.Count >= 2)
            Ability2.sprite = boughtImages[1].sprite;

        if (boughtImages.Count >= 3)
            Ability3.sprite = boughtImages[2].sprite;

        if (boughtImages.Count >= 4)
            Ability4.sprite = boughtImages[3].sprite;
    }
}
