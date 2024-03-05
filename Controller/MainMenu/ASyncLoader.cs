using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;


    public void LoadLevelBtn(string level)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        //start loading
        StartCoroutine(loadLevelASync(level));
    }

    IEnumerator loadLevelASync(string level)
    {
       //load level
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(level);
        //loop sampe operation slsai
        while (!loadOperation.isDone)
        {
            //itung progress
            float progressValue = Mathf.Clamp01(loadOperation.progress/0.9f);
            //update
            loadingSlider.value = progressValue;

            yield return null;
        }
    }

}
