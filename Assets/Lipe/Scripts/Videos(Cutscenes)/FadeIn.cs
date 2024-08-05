using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FadeIn : MonoBehaviour
{
    bool fadeIn;
    CanvasGroup canvasGroup;
    [SerializeField] float timeFade;
    [SerializeField] VideoPlayer videoPlayer;
    int proximaFase;

    private void Start()
    {
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
        canvasGroup = GetComponent<CanvasGroup>();
        fadeIn = false;
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        fadeIn = true;
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeFade * Time.deltaTime;
                if (canvasGroup.alpha == 1)
                {
                    proximaFase = 2;
                    GameManager.instance.LoadScene(proximaFase);
                }
            }
        }
        else
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeFade * Time.deltaTime;
            }
        }

    }
}
