using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAbilityController : MonoBehaviour
{
    public Image Ability1;
    public Image Ability2;
    public Image Ability3;
    public Image Ability4;
    public Image SelectedAbilityImage;
    public Image UnderSelectedAbilityImage;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public GameObject AButton1;
    public GameObject AButton2;
    public GameObject AButton3;
    public GameObject AButton4;
    public GameObject AButton5;
    public Image AbilityImage1;
    public Image AbilityImage2;
    public Image AbilityImage3;
    public Image AbilityImage4;
    public Image AbilityImage5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DisplayBoughtAbilities();
    }
    void DisplayBoughtAbilities()
    {
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
    public void SetSelectedAbilityImage(Image image)
    {
        SelectedAbilityImage.sprite = image.sprite;
    }
    public void SetUnderAbilityImage(Image image)
    {
        UnderSelectedAbilityImage.sprite = image.sprite;
    }
    public void ChangeAbility(Ability upAbility, Ability downAbility)
    {
    
    }

}
