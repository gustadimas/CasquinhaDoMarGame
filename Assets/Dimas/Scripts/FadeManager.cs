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
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, Mathf.Clamp01(elapsedTime / fadeDuration));
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, 1f - Mathf.Clamp01(elapsedTime / fadeDuration));
            yield return null;
        }
    }

    public void CarregarProximaCenaComFade()
    {
        int cenaAtual = SceneManager.GetActiveScene().buildIndex;
        int proximaCena = GameManager.proximaEtapa;

        Debug.Log($"Cena Atual: {cenaAtual}, Próxima Cena: {proximaCena}");

        if (proximaCena <= cenaAtual)
        {
            proximaCena = cenaAtual + 1;
            GameManager.proximaEtapa = proximaCena;
        }

        if (proximaCena < SceneManager.sceneCountInBuildSettings)
        {
            FadeToScene(proximaCena);
        }
        else
        {
            Debug.LogError("Proxima cena está fora do range das cenas configuradas no Build Settings.");
        }
    }
}
