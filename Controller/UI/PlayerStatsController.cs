using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI MaxHealthText;
    public TextMeshProUGUI AttackText;
    public TextMeshProUGUI GoldText;
    private int playerGold;
    PlayerStats ps;
    private void Start()
    {
        ps = PlayerStats.Instance;
    }
    // Update is called once per frame

    void Update()
    {
        LevelText.text = ps.Level.ToString();
        MaxHealthText.text = ps.MaxHealth.ToString();
        AttackText.text = ps.Attack.ToString();
        GoldText.text = ps.Gold.ToString();
    }
}
