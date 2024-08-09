using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Carregamento : MonoBehaviour
{
    [SerializeField] Slider barraProgresso;

    [Header("Configuração")]
    [SerializeField] float atraso;
    [SerializeField] float atrasoTexto;
    [SerializeField] float velocidadeRotacao;

    [Header("Componentes da UI")]
    [SerializeField] Image postit;
    [SerializeField] Image circulo;
    [SerializeField] TextMeshProUGUI textoCarregando;
    [SerializeField] GameObject canvasCarregamentoUI;
    bool telaCarregando = true;

    float tempo;

    private void Start()
    {
        tempo = 0;
        barraProgresso.value = 0;
        barraProgresso.maxValue = atraso;
        telaCarregando = true;
        StartCoroutine(AnimarTexto());
    }

    private void Update()
    {
        if (telaCarregando)
        {
            barraProgresso.value = Mathf.MoveTowards(barraProgresso.value, atraso, Time.deltaTime);
            tempo += Time.deltaTime;
            circulo.rectTransform.localEulerAngles -= new Vector3(0, 0, velocidadeRotacao * Time.deltaTime);
            if (tempo >= atraso)
            {
                telaCarregando = false;
                canvasCarregamentoUI.SetActive(false);
                this.enabled = false;
            }
        }
    }

    IEnumerator AnimarTexto()
    {
        for (int i = 0; telaCarregando; i++)
        {
            textoCarregando.text = "Carregando.";
            yield return new WaitForSeconds(atrasoTexto);
            textoCarregando.text = "Carregando..";
            yield return new WaitForSeconds(atrasoTexto);
            textoCarregando.text = "Carregando...";
            yield return new WaitForSeconds(atrasoTexto);
        }
    }
}