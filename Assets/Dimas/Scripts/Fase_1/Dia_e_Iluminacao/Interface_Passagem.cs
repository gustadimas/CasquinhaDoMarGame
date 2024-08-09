using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class Interface_Passagem : MonoBehaviour
{
    [Header("Configuracoes de Desaparecimento")]
    [SerializeField] float tempoDesaparecerTexto = 2f;
    [SerializeField] float tempoDesaparecerPainel = .6f;
    [SerializeField] float atraso = 1f;

    [Header("Elementos da Interface")]
    [SerializeField] CanvasGroup painelDias;
    [SerializeField] TextMeshProUGUI textoDias, textoCompleto;

    public static Interface_Passagem Instance { get; private set; }

    [SerializeField] Transform jogador;
    [SerializeField] Transform posicaoInicialJogador;

    private void Awake()
    {
        Instance = this;

        jogador = GameObject.FindObjectOfType<Player_Pesquisador>().transform;

        if (jogador != null)
            posicaoInicialJogador.position = jogador.position;

        textoDias.text = "DIA 01";
        StartCoroutine(Desaparecer());
    }

    private IEnumerator Desaparecer()
    {
        if (GerenciadorDeIluminacao.atualizarDia == false)
        {
            yield return new WaitForSeconds(atraso);

            float _tempoPassado = 0f;
            while (_tempoPassado < tempoDesaparecerTexto)
            {
                _tempoPassado += Time.deltaTime;
                textoDias.alpha = Mathf.Lerp(1, 0, _tempoPassado / tempoDesaparecerTexto);
                yield return null;
            }
            textoDias.alpha = 0;

            GerenciadorDeIluminacao.atualizarDia = true;

            _tempoPassado = 0f;
            while (_tempoPassado < tempoDesaparecerPainel)
            {
                _tempoPassado += Time.deltaTime;
                painelDias.alpha = Mathf.Lerp(1, 0, _tempoPassado / tempoDesaparecerPainel);
                yield return null;
            }
            painelDias.alpha = 0;
        }
    }

    public IEnumerator Aparecer()
    {
        painelDias.alpha = 0;
        textoDias.alpha = 0;

        float _tempoPassado = 0f;
        while (_tempoPassado < tempoDesaparecerTexto)
        {
            _tempoPassado += Time.deltaTime;
            textoDias.alpha = Mathf.Lerp(0, 1, _tempoPassado / tempoDesaparecerTexto);
            yield return null;
        }
        textoDias.alpha = 1;

        _tempoPassado = 0f;
        while (_tempoPassado < tempoDesaparecerPainel)
        {
            _tempoPassado += Time.deltaTime;
            painelDias.alpha = Mathf.Lerp(0, 1, _tempoPassado / tempoDesaparecerPainel);
            yield return null;
        }

        jogador.position = posicaoInicialJogador.position;

        painelDias.alpha = 1;

        GameManager.diasCompletos += 1;
        Debug.LogError("Dias Completos: " + GameManager.diasCompletos);

        GerenciadorDeIluminacao.atualizarDia = false;

        if (GameManager.diasCompletos < 3)
        {
            GerenciadorDeIluminacao.instance.ReiniciarDia();
            textoDias.text = "DIA " + QuestController.instance.diaAtual.ToString("00");
            StartCoroutine(Desaparecer());
        }
        else
        {
            textoDias.text = "";
            textoCompleto.text = "PERÍODO DE INCUBAÇÃO COMPLETO";
            //textoCompleto.color = Color.green;
            Invoke(nameof(CarregarProximaCena), 5f);
        }
    }

    [ContextMenu("Passar Cena")]
    private void CarregarProximaCena()
    {
        FadeManager.instance.CarregarProximaCenaComFade();
    }
}