using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnuncioEventos : MonoBehaviour
{

    [Header("Configuracoes de Desaparecimento")]
    [SerializeField] float tempo;

    [Header("Elementos UI")]
    [SerializeField] GameObject painelEventos;
    [SerializeField] TextMeshProUGUI txtAnuncio;
    [SerializeField] GameObject painelDias;

    EventController eventController;
    void Start()
    {
        eventController = FindObjectOfType<EventController>();
        eventController.OnRandomizedEvent += EventController_OnRandomizedEvent;

        painelEventos.SetActive(false);
    }

    private void EventController_OnRandomizedEvent(string evento)
    {
        StartCoroutine(VerificarPainelDias());
        if (evento == "EventoLixo")
        {
            switch (QuestController.instance.diaAtual)
            {
                case 1:
                    txtAnuncio.text = "Existem alguns lixos espalhados pela praia. V� coletar!";
                    break;

                case 2:
                    txtAnuncio.text = "Colete mais lixo pela praia. Dessa vez, mais do que antes!";
                    break;

                case 3:
                    txtAnuncio.text = "Quanta sujeira! Tem mais lixos do que nunca espalhados pela praia. Colete todos!";
                    break;
            }
        }

        if (evento == "EventoTurista")
        {
            switch (QuestController.instance.diaAtual)
            {
                case 1:
                    txtAnuncio.text = "Uma turista rec�m-chegada est� visitando o Projeto Tamar. Sorria e v� dar as boas-vindas!";
                    break;

                case 2:
                    txtAnuncio.text = "A turista est� jogando bola bem ao lado de uma �rea de desova demarcada. V� falar com ela!";
                    break;

                case 3:
                    txtAnuncio.text = "Emerg�ncia! A turista acaba de encontrar um ninho de tartaruga marinha revirado. V� verificar imediatamente!";
                    break;
            }
        }

        if (evento == "EventoPescador")
        {
            switch (QuestController.instance.diaAtual)
            {
                case 1:
                    txtAnuncio.text = "O pescador est� pescando com anz�is bem pr�ximo a costa. V� falar com ele antes que um filhote de tartaruga seja pego!";
                    break;

                case 2:
                    txtAnuncio.text = "O pescador acaba de voltar da pescaria e est� jogando os restos dos peixes na costa. V� conversar com ele!";
                    break;

                case 3:
                    txtAnuncio.text = "Alerta! Alerta! O pescador foi flagrado roubando ovos de tartaruga direto do ninho. Cuide da situa��o!";
                    break;
            }
        }
    }

    IEnumerator VerificarPainelDias()
    {
        yield return new WaitUntil(() => painelDias.GetComponent<CanvasGroup>().alpha == 0);
        StartCoroutine(Aparecer());
    }

    IEnumerator Aparecer()
    {
            CanvasGroup canvasGroup = painelEventos.GetComponent<CanvasGroup>();
            float tempoPassado = 0;

            painelEventos.SetActive(true);

            while (tempoPassado < tempo)
            {
                tempoPassado += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(0, 1, tempoPassado / tempo);
                yield return null;
            }

            canvasGroup.alpha = 1;


            yield return new WaitForSeconds(tempo);

            StartCoroutine(Desaparecer());
        
    }

    IEnumerator Desaparecer()
    {
        CanvasGroup canvasGroup = painelEventos.GetComponent<CanvasGroup>();
        float tempoPassado = 0;

        while (tempoPassado < tempo)
        {
            tempoPassado += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, tempoPassado / tempo);
            yield return null;
        }

        canvasGroup.alpha = 0;
        painelEventos.SetActive(false);
    }
}
