using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    bool fadeIn;
    CanvasGroup canvasGroup;
    [SerializeField] float timeFade;
    [SerializeField] VideoPlayer videoPlayer;

    float tempoUltimoToque = 0f;
    const float tempoDuplotoque = 0.3f;

    private void Start()
    {
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
        canvasGroup = GetComponent<CanvasGroup>();
        fadeIn = false;
    }

    private void Update()
    {
        DuploToque();

        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeFade * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    canvasGroup.alpha = 1;
                    CarregarProximaCena();
                }
            }
        }
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        fadeIn = true;
    }

    private void DuploToque()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (Time.time - tempoUltimoToque < tempoDuplotoque)
            {
                PularVideo();
            }
            tempoUltimoToque = Time.time;
        }
    }

    private void PularVideo()
    {
        fadeIn = true;
        canvasGroup.alpha = 1;
        CarregarProximaCena();
    }

    private void CarregarProximaCena()
    {
        int cenaAtual = SceneManager.GetActiveScene().buildIndex;
        int proximaCena = GameManager.proximaEtapa;

        Debug.Log($"Cena Atual: {cenaAtual}, Próxima Cena: {proximaCena}");
        if (cenaAtual != 8)
        {
            if (proximaCena <= cenaAtual)
            {
                proximaCena = cenaAtual + 1;
                GameManager.proximaEtapa = proximaCena;
            }
        }
        else
        {
            proximaCena = 0;
            GameManager.proximaEtapa = proximaCena;
        }

        if (proximaCena < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(proximaCena);
        else
            Debug.LogError("Proxima cena está fora do range das cenas configuradas no Build Settings.");
    }
}