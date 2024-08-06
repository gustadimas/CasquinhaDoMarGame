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

    private void Start()
    {
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
        canvasGroup = GetComponent<CanvasGroup>();
        fadeIn = false;
        GameManager.proximaEtapa = 1;
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
                    GameManager.proximaEtapa++;
                    GameManager.instance.LoadScene(GameManager.proximaEtapa);
                }
            }
        }
    }
}
