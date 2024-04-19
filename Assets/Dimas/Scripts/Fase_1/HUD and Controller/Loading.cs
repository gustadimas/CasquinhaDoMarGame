using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Loading : MonoBehaviour
{

    [SerializeField] private Slider progressSlider;


    [Header("Config")]
    [SerializeField] private float delay;
    [SerializeField] private float delayText;
    [SerializeField] private float velocityRotation;

    [Header("Link UI Components")]
    [SerializeField] private Image postit;
    [SerializeField] private Image circle;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private GameObject loadingCanvasUI;
    private bool loadScreen = true;

    private float time;


    private void Start()
    {
        time = 0;
        progressSlider.value = 0;
        progressSlider.maxValue = delay;
        loadScreen = true;
        StartCoroutine(AnimText());
        
    }

    private void Update()
    {
        if (loadScreen)
        {
            progressSlider.value = Mathf.MoveTowards(progressSlider.value, delay, Time.deltaTime);
            time += Time.deltaTime;
            circle.rectTransform.localEulerAngles -= new Vector3(0, 0, velocityRotation * Time.deltaTime);
            if (time >= delay)
            {
                loadScreen = false;
                loadingCanvasUI.SetActive(false);
                this.enabled = false;
            }
        }
    }

    IEnumerator AnimText()
    {
        for (int i = 0; loadScreen; i++)
        {
            loadingText.text = "Carregando.";
            yield return new WaitForSeconds(delayText);
            loadingText.text = "Carregando..";
            yield return new WaitForSeconds(delayText);
            loadingText.text = "Carregando...";
            yield return new WaitForSeconds(delayText);
        }
    }
}
