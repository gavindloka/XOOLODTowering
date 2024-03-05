using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public GameObject StatsCanvasToggle;
    public GameObject ShopCanvasToggle;
    public GameObject ChangeCanvasToggle;
    public GameObject RebindCanvasToggle;
    public GameObject EnterMazeCanvasToggle;
    public GameObject FightBossCanvasToggle;
    public GameObject EndTeleporterCanvasToggle;
    public GameObject WinBossCanvasToggle;

    public bool isStatsCanvasActive = false;
    public bool isShopCanvasActive = false;
    public bool isChangeCanvasActive = false;
    public bool isRebindCanvasActive = false;
    public bool isEnterMazeCanvasActive = false;
    public bool isFightBossCanvasActive = false;
    public bool isEndTeleporterCanvasActive = false;
    public bool isWinBossCanvasActive = false;


    private NPCController sherylNpcController;
    private NPCController robertNpcController;
    private NPCController nobolNpcController;
    private PlayerMovement pm;

    private bool WasOnCircle = false;
    private bool WasOnEndTeleporter = false;



    private void Start()
    {
        GameObject sheryl = GameObject.FindGameObjectWithTag("SherylNPC");
        GameObject robert = GameObject.FindGameObjectWithTag("RobertNPC");
        GameObject nobol = GameObject.FindGameObjectWithTag("NobolNPC");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (sheryl != null)
        {
            sherylNpcController = sheryl.GetComponent<NPCController>();
        }
        if (robert != null)
        {
            robertNpcController = robert.GetComponent<NPCController>();
        }
        if (player != null)
        {
            pm = player.GetComponent<PlayerMovement>();
        }
        if (nobol != null)
        {
            nobolNpcController = nobol.GetComponent<NPCController>();
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (StatsCanvasToggle != null)
            {
                isStatsCanvasActive = !isStatsCanvasActive;
                StatsCanvasToggle.SetActive(isStatsCanvasActive);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ShopCanvasToggle != null && sherylNpcController != null && sherylNpcController.IsWithinInteractRange &&!PlayerMovement.CombatToggle)
            {

                isShopCanvasActive = !isShopCanvasActive;
                ShopCanvasToggle.SetActive(isShopCanvasActive);

            }
            else if (ChangeCanvasToggle != null && sherylNpcController != null && robertNpcController.IsWithinInteractRange && !PlayerMovement.CombatToggle)
            {
                isChangeCanvasActive = !isChangeCanvasActive;
                ChangeCanvasToggle.SetActive(isChangeCanvasActive);
            }
            else if (FightBossCanvasToggle != null && nobolNpcController != null && nobolNpcController.IsWithinInteractRange && !PlayerMovement.CombatToggle)
            {
                isFightBossCanvasActive = !isFightBossCanvasActive;
                FightBossCanvasToggle.SetActive(isFightBossCanvasActive);
            }
        }


        if (Input.GetKeyDown(KeyCode.Equals))
        {
            if (RebindCanvasToggle != null)
            {
                isRebindCanvasActive = !isRebindCanvasActive;
                RebindCanvasToggle.SetActive(isRebindCanvasActive);
            }
        }
        if (pm.IsOnCircle && !isEnterMazeCanvasActive && !WasOnCircle)
        {
            isEnterMazeCanvasActive = true;
            EnterMazeCanvasToggle.SetActive(true);
            WasOnCircle = true;
        }else if (!pm.IsOnCircle)
        {
            WasOnCircle = false;
        }
        if (pm.IsOnEndTeleporter && !isEndTeleporterCanvasActive &&!WasOnEndTeleporter)
        {
            isEndTeleporterCanvasActive= true;
            EndTeleporterCanvasToggle.SetActive(true);
            WasOnEndTeleporter = true;
        }else if (!pm.IsOnEndTeleporter)
        {
            WasOnEndTeleporter = false;
        }
        if (Boss.isBossDead)
        {
            WinBossCanvasToggle.SetActive(true);
            StartCoroutine(CloseWinBossCanvas());
        }

    }
    IEnumerator CloseWinBossCanvas() { 
            yield return new WaitForSeconds(5f);
            WinBossCanvasToggle.SetActive(false);
            SceneManager.LoadScene("MainScene");
    }
    public void CloseShopCanvas()
    {
        if (ShopCanvasToggle != null && !Input.GetKeyDown(KeyCode.Space))
        {
            isShopCanvasActive = false;
            ShopCanvasToggle.SetActive(false);
        }
    }
    public void CloseChangeCanvas()
    {
        if (ChangeCanvasToggle != null && !Input.GetKeyDown(KeyCode.Space))
        {
            isChangeCanvasActive = false;
            ChangeCanvasToggle.SetActive(false);
        }
    }
    public void CloseRebindCanvas()
    {
        if (RebindCanvasToggle != null && !Input.GetKeyDown(KeyCode.Space))
        {
            isRebindCanvasActive = false;
            RebindCanvasToggle.SetActive(false);
        }
    }
    public void CloseEnterMazeCanvas()
    {
        if (EnterMazeCanvasToggle != null && WasOnCircle && !Input.GetKeyDown(KeyCode.Space))
        {
            isEnterMazeCanvasActive = false;
            EnterMazeCanvasToggle.SetActive(false);
        }
    }
    public void CloseEndTeleporterCanvas()
    {
        if(EndTeleporterCanvasToggle!=null&& WasOnEndTeleporter && !Input.GetKeyDown(KeyCode.Space)){
            isEndTeleporterCanvasActive = false;
            EndTeleporterCanvasToggle.SetActive(false);
        }
    }
    public void CloseFightBossCanvas()
    {
        if (FightBossCanvasToggle != null && !Input.GetKeyDown(KeyCode.Space))
        {
            isFightBossCanvasActive = false;
            FightBossCanvasToggle.SetActive(false);
        }
    }
    public void EnterMain()
    {
        if(EndTeleporterCanvasToggle!=null&WasOnEndTeleporter && !Input.GetKeyDown(KeyCode.Space))
        {
            isEndTeleporterCanvasActive = false;
            EndTeleporterCanvasToggle.SetActive(false);
            SceneManager.LoadScene("MainScene");

        }
    }
    public void EnterMaze()
    {
        if (EnterMazeCanvasToggle != null && WasOnCircle && !Input.GetKeyDown(KeyCode.Space))
        {
            isEnterMazeCanvasActive = false;
            EnterMazeCanvasToggle.SetActive(false);
            SceneManager.LoadScene("MazeScene");
        }
    }
    public void EnterBoss()
    {
        if (FightBossCanvasToggle != null &&  !Input.GetKeyDown(KeyCode.Space))
        {
            isFightBossCanvasActive = false;
            FightBossCanvasToggle.SetActive(false);
            SceneManager.LoadScene("BossScene");
        }
    }
}
