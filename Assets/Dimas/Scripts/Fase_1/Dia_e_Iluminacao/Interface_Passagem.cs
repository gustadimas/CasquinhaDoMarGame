using UnityEngine;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class Interface_Passagem : MonoBehaviour
{
    [Header("Configuracoes de Desaparecimento")]
    [SerializeField] float tempoDesaparecerTexto = 2f;
    [SerializeField] float tempoDesaparecerPainel = .6f;
    [SerializeField] float atraso = 1f;

    [Header("Elementos da Interface")]
    [SerializeField] CanvasGroup painelDias;
    [SerializeField] TextMeshProUGUI texto;

    int numDia = 1;
    public Action comecarDia;
    public static Interface_Passagem instance { get; private set; }

    void Awake()
    {
        texto.text = "DIA 01";
        instance = this;
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
                texto.alpha = Mathf.Lerp(1, 0, _tempoPassado / tempoDesaparecerTexto);
                yield return null;
            }
            texto.alpha = 0;

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
        texto.alpha = 0;

        float _tempoPassado = 0f;
        while (_tempoPassado < tempoDesaparecerTexto)
        {
            _tempoPassado += Time.deltaTime;
            texto.alpha = Mathf.Lerp(0, 1, _tempoPassado / tempoDesaparecerTexto);
            yield return null;
        }
        texto.alpha = 1;

        _tempoPassado = 0f;
        while (_tempoPassado < tempoDesaparecerPainel)
        {
            _tempoPassado += Time.deltaTime;
            painelDias.alpha = Mathf.Lerp(0, 1, _tempoPassado / tempoDesaparecerPainel);
            yield return null;
        }
        painelDias.alpha = 1;

        numDia++;
        texto.text = "DIA " + numDia.ToString("00");
        GerenciadorDeIluminacao.instance.ReiniciarDia();
        GerenciadorDeIluminacao.atualizarDia = false;
        StartCoroutine(Desaparecer());
    }
}