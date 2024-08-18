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

    void VideoPlayer_loopPointReached(VideoPlayer source) => fadeIn = true;

    void DuploToque()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (Time.time - tempoUltimoToque < tempoDuplotoque)
                PularVideo();
            tempoUltimoToque = Time.time;
        }
    }

    void PularVideo()
    {
        fadeIn = true;
        canvasGroup.alpha = 1;
        CarregarProximaCena();
    }

    void CarregarProximaCena()
    {
        int _cenaAtual = SceneManager.GetActiveScene().buildIndex;
        int _proximaCena = GameManager.proximaEtapa;
        Debug.Log($"Cena Atual: {_cenaAtual}, Próxima Cena: {_proximaCena}");
        if (_cenaAtual != 8)
        {
            if (_proximaCena <= _cenaAtual)
            {
                _proximaCena = _cenaAtual + 1;
                GameManager.proximaEtapa = _proximaCena;
            }
        }
        else
        {
            _proximaCena = 0;
            GameManager.proximaEtapa = _proximaCena;
        }

        if (_proximaCena < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(_proximaCena);
        else
            Debug.LogError("Proxima cena está fora do range das cenas configuradas no Build Settings.");
    }
}