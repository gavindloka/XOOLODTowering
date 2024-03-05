using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatController : MonoBehaviour
{
    private string[] respawnCode, unlimitedGoldCode, speedCode;
    private int respawnIndex, goldIndex, speedIndex;
    public GameObject RespawnCheatCanvas;
    public GameObject NJUSCheatCanvas;
    public GameObject DEPDEPDEPCheatCanvas;
    PlayerStats ps;

    void Start()
    {
        respawnCode = new string[] { "2", "3", "-", "1" };
        unlimitedGoldCode = new string[] { "N", "J", "U", "S" };
        speedCode = new string[] { "D", "E", "P", "D", "E", "P", "D", "E", "P" };
        respawnIndex = 0;
        goldIndex = 0;
        speedIndex = 0;
        ps = PlayerStats.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(respawnCode[respawnIndex]))
            {
                respawnIndex++;
                if (respawnIndex==respawnCode.Length)
                {
                    ActivateCheatCanvas(RespawnCheatCanvas);
                    StartCoroutine(DeactivateCheatCanvas(RespawnCheatCanvas, 5f));
                }
            }
            else
            {
                respawnIndex = 0;
            }

            string inputKey = Input.inputString.ToUpper();

            if (goldIndex < unlimitedGoldCode.Length && inputKey == unlimitedGoldCode[goldIndex])
            {
                goldIndex++;
                if (goldIndex==unlimitedGoldCode.Length)
                {
                    ActivateCheatCanvas(NJUSCheatCanvas);
                    StartCoroutine(DeactivateCheatCanvas(NJUSCheatCanvas,5f));
                }
            }
            else
            {
                goldIndex = 0;
            }

            if (speedIndex < speedCode.Length && inputKey == speedCode[speedIndex])
            {
                speedIndex++;
                if (speedIndex==speedCode.Length)
                {
                    ActivateCheatCanvas(DEPDEPDEPCheatCanvas);
                    StartCoroutine(DeactivateCheatCanvas(DEPDEPDEPCheatCanvas,5f));
                }
            }
            else
            {
                speedIndex = 0;
            }
        }

        if (respawnIndex == respawnCode.Length)
        {
            SceneManager.LoadScene("MainScene");
            respawnIndex = 0;
        }
        else if (goldIndex == unlimitedGoldCode.Length)
        {
            goldIndex = 0;
            ps.Gold += 231231231;
            Debug.Log("PlayerGOLDCheat: "+ps.Gold);
            
        }
        else if (speedIndex == speedCode.Length)
        {
            Debug.Log("I am speed");
            speedIndex = 0;
            PlayerMovement.BaseSpeed *= 250 / 100;
        }
    }
    public void ActivateCheatCanvas(GameObject canvas)
    {
        if (canvas!=null)
        {
            canvas.SetActive(true);
        }
    }
    IEnumerator DeactivateCheatCanvas(GameObject canvas, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }
}
