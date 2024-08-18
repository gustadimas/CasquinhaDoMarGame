using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    [SerializeField] Image imagemFade;
    [SerializeField] float duracaoFade = 4f;
    [SerializeField] bool usarUnscaledTime = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            imagemFade.enabled = false;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (imagemFade != null)
        {
            imagemFade.color = new Color(0f, 0f, 0f, 1f);
            StartCoroutine(DesaparecerFade());
        }
        else
        {
            Debug.LogError("A imagem de fade não está atribuída no GerenciadorDeFade.");
        }
    }

    public void FazerFadeParaCena(int indiceCena) => StartCoroutine(FazerFadeECarregarCena(indiceCena));

    private IEnumerator FazerFadeECarregarCena(int indiceCena)
    {
        float _tempoDecorrido = 0f;

        while (_tempoDecorrido < duracaoFade)
        {
            _tempoDecorrido += usarUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            imagemFade.color = new Color(0f, 0f, 0f, Mathf.Clamp01(_tempoDecorrido / duracaoFade));
            yield return null;
        }

        SceneManager.LoadScene(indiceCena);

        StartCoroutine(DesaparecerFade());
    }

    private IEnumerator DesaparecerFade()
    {
        float _tempoDecorrido = 0f;

        while (_tempoDecorrido < duracaoFade)
        {
            _tempoDecorrido += usarUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            imagemFade.color = new Color(0f, 0f, 0f, 1f - Mathf.Clamp01(_tempoDecorrido / duracaoFade));
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
            FazerFadeParaCena(_proximaCena);
        else
            Debug.LogError("Próxima cena está fora do range das cenas configuradas no Build Settings.");
    }

    public void CarregarCenaAnteriorComFade()
    {
        int _cenaAtual = SceneManager.GetActiveScene().buildIndex;
        int _cenaAnterior = _cenaAtual - 1;

        if (_cenaAnterior >= 0)
        {
            Debug.Log($"Cena Atual: {_cenaAtual}, Cena Anterior: {_cenaAnterior}");
            FazerFadeParaCena(_cenaAnterior);
        }
        else
            Debug.LogWarning("Não existe uma cena anterior para carregar.");
    }

    public void CarregarCenaAtualComFade()
    {
        int _cenaAtual = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"Recarregando Cena Atual: {_cenaAtual}");
        FazerFadeParaCena(_cenaAtual);
    }

    public void ConfigurarUnscaledTime(bool usarUnscaled) => usarUnscaledTime = usarUnscaled;
}