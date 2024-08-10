using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            fadeImage.enabled = false;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (fadeImage != null)
        {
            fadeImage.color = new Color(0f, 0f, 0f, 1f);
            StartCoroutine(FadeOut());
        }
        else
        {
            Debug.LogError("Fade Image is not assigned in the FadeManager.");
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        StartCoroutine(FadeInAndLoadScene(sceneIndex));
    }

    private IEnumerator FadeInAndLoadScene(int sceneIndex)
    {
        float _elapsedTime = 0f;

        while (_elapsedTime < fadeDuration)
        {
            _elapsedTime += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, Mathf.Clamp01(_elapsedTime / fadeDuration));
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float _elapsedTime = 0f;

        while (_elapsedTime < fadeDuration)
        {
            _elapsedTime += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, 1f - Mathf.Clamp01(_elapsedTime / fadeDuration));
            yield return null;
        }
    }

    public void CarregarProximaCenaComFade()
    {
        int _cenaAtual = SceneManager.GetActiveScene().buildIndex;
        int _proximaCena = GameManager.proximaEtapa;

        Debug.Log($"Cena Atual: {_cenaAtual}, Próxima Cena: {_proximaCena}");

        if (_proximaCena <= _cenaAtual)
        {
            _proximaCena = _cenaAtual + 1;
            GameManager.proximaEtapa = _proximaCena;
        }

        if (_proximaCena < SceneManager.sceneCountInBuildSettings)
            FadeToScene(_proximaCena);
        else
            Debug.LogError("Proxima cena está fora do range das cenas configuradas no Build Settings.");
    }

    public void CarregarCenaAnteriorComFade()
    {
        int _cenaAtual = SceneManager.GetActiveScene().buildIndex;
        int _cenaAnterior = _cenaAtual - 1;

        if (_cenaAnterior >= 0)
        {
            Debug.Log($"Cena Atual: {_cenaAtual}, Cena Anterior: {_cenaAnterior}");
            FadeToScene(_cenaAnterior);
        }
        else
        {
            Debug.LogWarning("Não existe uma cena anterior para carregar.");
        }
    }

    public void CarregarCenaAtualComFade()
    {
        int _cenaAtual = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"Recarregando Cena Atual: {_cenaAtual}");
        FadeToScene(_cenaAtual);
    }
}