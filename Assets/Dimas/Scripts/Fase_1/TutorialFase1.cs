using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialFase1 : MonoBehaviour
{
    [Header("Componentes do Tutorial")]
    [SerializeField] GameObject painelTutorial;
    [SerializeField] CanvasGroup canvasGroupTutorial;
    [SerializeField] TextMeshProUGUI textoTutorial;
    [SerializeField] GameObject botaoPularTutorial;
    [SerializeField] GameObject analogicoMovimento;
    [SerializeField] GameObject botaoInteragir;
    [SerializeField] GameObject painelMissoes;

    [Header("Componentes do Jogo")]
    [SerializeField] Player_Pesquisador playerPesquisador;
    [SerializeField] GerenciadorDeIluminacao gerenciadorIluminacao;

    Transform parentOriginalAnalogico;
    Transform parentOriginalBotaoInteragir;
    Transform parentOriginalPainelMissoes;

    float tempoEtapas = 4f;
    float fadeDuration = 2f;
    bool pularTutorial = false;

    private void Start()
    {
        canvasGroupTutorial.alpha = 0;

        AudioListener.volume = 0f;

        parentOriginalAnalogico = analogicoMovimento.transform.parent;
        parentOriginalBotaoInteragir = botaoInteragir.transform.parent;
        parentOriginalPainelMissoes = painelMissoes.transform.parent;

        playerPesquisador.desativarEntradas = true;
        GerenciadorDeIluminacao.atualizarDia = false;
        gerenciadorIluminacao.horaDoDia = 6;

        botaoPularTutorial.SetActive(true);
        botaoPularTutorial.GetComponent<Button>().onClick.AddListener(PularTutorial);

        StartCoroutine(ExecutarTutorial());
    }

    void PularTutorial()
    {
        pularTutorial = true;
        ConcluirTutorial();
    }

    IEnumerator ExecutarTutorial()
    {
        yield return new WaitForSecondsRealtime(tempoEtapas);

        if (pularTutorial)
        {
            yield break;
        }

        Time.timeScale = 0f;

        painelTutorial.SetActive(true);
        botaoPularTutorial.SetActive(true);

        yield return StartCoroutine(FazerFadeIn(canvasGroupTutorial, fadeDuration));

        // Introdução
        yield return ExibirMensagem("Bem-vindo ao período de incubação! Vamos aprender os controles básicos antes de começar.");
        if (pularTutorial) yield break;

        // Tutorial de Movimento
        yield return ExibirMensagem("Use o analógico à esquerda para mover seu personagem.");
        if (pularTutorial) yield break;

        DestacarObjeto(analogicoMovimento, false);
        yield return new WaitForSecondsRealtime(tempoEtapas);
        RestaurarObjeto(analogicoMovimento, parentOriginalAnalogico, true);

        // Tutorial de Interação
        yield return ExibirMensagem("Use o botão à direita para interagir com objetos que possuem um contorno (<color=#00FF00>outline</color>).");
        if (pularTutorial) yield break;

        DestacarObjeto(botaoInteragir, false);
        yield return new WaitForSecondsRealtime(tempoEtapas);
        RestaurarObjeto(botaoInteragir, parentOriginalBotaoInteragir, true);

        // Tutorial do Painel de Missões
        yield return ExibirMensagem("O botão acima à direita abre o painel de missões. Verifique suas missões a qualquer momento.");
        if (pularTutorial) yield break;

        DestacarObjeto(painelMissoes, false);
        yield return new WaitForSecondsRealtime(tempoEtapas);
        RestaurarObjeto(painelMissoes, parentOriginalPainelMissoes, true);

        // Conclusão do Tutorial
        yield return ExibirMensagem("Você está pronto! O dia irá começar agora!");
        ConcluirTutorial();
    }

    void ConcluirTutorial()
    {
        StartCoroutine(FazerFadeOut(canvasGroupTutorial, fadeDuration));

        AudioListener.volume = 1f;
        Time.timeScale = 1f;

        painelTutorial.SetActive(false);

        playerPesquisador.desativarEntradas = false;
        GerenciadorDeIluminacao.atualizarDia = true;
        gerenciadorIluminacao.duracaoDiaMinutos *= 1.5f;

        botaoPularTutorial.SetActive(false);
    }

    void DestacarObjeto(GameObject objeto, bool interagivel)
    {
        objeto.transform.SetParent(painelTutorial.transform);

        if (objeto.GetComponent<Button>() != null)
            objeto.GetComponent<Button>().interactable = interagivel;

        else if (objeto.GetComponent<CanvasGroup>() != null)
        {
            objeto.GetComponent<CanvasGroup>().interactable = interagivel;
            objeto.GetComponent<CanvasGroup>().blocksRaycasts = interagivel;
        }
    }

    void RestaurarObjeto(GameObject objeto, Transform parentOriginal, bool interagivel)
    {
        objeto.transform.SetParent(parentOriginal);

        if (objeto.GetComponent<Button>() != null)
            objeto.GetComponent<Button>().interactable = interagivel;

        else if (objeto.GetComponent<CanvasGroup>() != null)
        {
            objeto.GetComponent<CanvasGroup>().interactable = interagivel;
            objeto.GetComponent<CanvasGroup>().blocksRaycasts = interagivel;
        }
    }

    IEnumerator ExibirMensagem(string mensagem)
    {
        textoTutorial.text = mensagem;
        yield return new WaitForSecondsRealtime(tempoEtapas);
    }

    IEnumerator FazerFadeIn(CanvasGroup canvasGroup, float duracao)
    {
        float tempoPassado = 0f;
        while (tempoPassado < duracao)
        {
            tempoPassado += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, tempoPassado / duracao);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    IEnumerator FazerFadeOut(CanvasGroup canvasGroup, float duracao)
    {
        float tempoPassado = 0f;
        while (tempoPassado < duracao)
        {
            tempoPassado += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, tempoPassado / duracao);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}