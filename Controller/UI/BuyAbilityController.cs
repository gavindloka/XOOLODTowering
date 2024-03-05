using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuyAbilityController : MonoBehaviour
{
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionTitleText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI PriceText;
    public Image AbilityImageSelected;


    public Image HorizontalImage;
    public Image RedEnergyImage;
    public Image MeteorShowerImage;
    public Image LaserRainImage;
    public Image HollowRedImage;
    private int playerGold;
    private Ability selectedAbility;
    PlayerStats ps;
    

    public List<Ability> boughtAbilities = new List<Ability>();
    public static List<Image> boughtImages = new List<Image>();

    void Start()
    {
        ps = PlayerStats.Instance;
        selectedAbility = Ability.Instance;
    }
    public void ShowAbilityInfo(Ability ability, Image abilityImage)
    {
        selectedAbility = ability;
        TitleText.text = ability.Name;
        DescriptionTitleText.text = "Description: ";
        DescriptionText.text = ability.Description;
        AbilityImageSelected.sprite = abilityImage.sprite;
        PriceText.text = ability.Price.ToString();
    }
    public void ShowHorizontalSlashInfo()
    {
        ShowAbilityInfo(new HorizontalSlash(), HorizontalImage);
    }

    public void ShowRedEnergyInfo()
    {
        ShowAbilityInfo(new RedEnergyExplosion(), RedEnergyImage);
    }
    public void ShowMeteorShowerInfo()
    {
        ShowAbilityInfo(new MeteorShower(),MeteorShowerImage);
    }
    public void ShowLaserRainInfo()
    {
        ShowAbilityInfo(new LaserRain(),LaserRainImage);
    }
    public void ShowHollowRedInfo()
    {
        ShowAbilityInfo(new HollowRed(), HollowRedImage);
    }
    // Update is called once per frame
    void Update()
    {
        playerGold = ps.Gold;
        Debug.Log("Player gold: "+playerGold);
        if (selectedAbility.Price > playerGold)
        {
            PriceText.color = Color.red;
        }
        if (selectedAbility.Price <= playerGold)
        {
            PriceText.color = Color.yellow;
        }
    }
    public void BuyAbility()
    {
        //&& selectedAbility.Price <= playerGold
        if (selectedAbility.Price <=playerGold && boughtAbilities.Count<=4)
        {
            playerGold -= selectedAbility.Price;
            Debug.Log("Ability purchased: " + selectedAbility.Name);
            AbilityImageSelected.sprite = AbilityImageSelected.sprite;
            boughtAbilities.Add(selectedAbility);
            Image abilityImageCopy = Instantiate(AbilityImageSelected);
            boughtImages.Add(abilityImageCopy);
        }
        else
        {
            Debug.Log("Insufficient gold to buy ability: " + selectedAbility.Name);
        }
    }
}
