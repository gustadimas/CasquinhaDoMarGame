using UnityEngine;
using System.Collections;
using TMPro;

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

    void Awake()
    {
        textoDias.text = "DIA 01";
        Instance = this;
        StartCoroutine(Desaparecer());
    }

    IEnumerator Desaparecer()
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

            _tempoPassado = 0f;

            while (_tempoPassado < tempoDesaparecerPainel)
            {
                _tempoPassado += Time.deltaTime;
                painelDias.alpha = Mathf.Lerp(1, 0, _tempoPassado / tempoDesaparecerPainel);
                yield return null;
            }

            painelDias.alpha = 0;
            GerenciadorDeIluminacao.atualizarDia = true;
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
            textoCompleto.color = Color.green;
            Invoke(nameof(EstagioCompleto), 5f);
        }
    }

    public void EstagioCompleto()
    {
        GameManager.proximaEtapa++;
        GameManager.instance.LoadScene(GameManager.proximaEtapa);
    }
}