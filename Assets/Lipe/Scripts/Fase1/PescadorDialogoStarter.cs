using cherrydev;
using System.Collections;
using UnityEngine;

public class PescadorDialogoStarter : MonoBehaviour
{
    [SerializeField] DialogBehaviour scriptDialogo;
    [SerializeField] DialogNodeGraph[] graficoNos;
    [SerializeField] Player_Pesquisador scriptMovimentacao;
    [SerializeField] Cam_Primeira_Pessoa scriptCamera;
    [SerializeField] GameObject analogico, btnInteragir;
    [SerializeField] Transform posicaoJogador;
    [SerializeField] private int missaoID;

    bool acertou;
    void Start()
    {
        scriptDialogo = GameObject.FindObjectOfType<DialogBehaviour>();
        scriptMovimentacao = GameObject.FindObjectOfType<Player_Pesquisador>();
        scriptCamera = GameObject.FindObjectOfType<Cam_Primeira_Pessoa>();
        analogico = GameObject.Find("Analogico");
        btnInteragir = GameObject.Find("Interface_Btn");
        posicaoJogador = transform.Find("Pesquisador");

        acertou = false;
        switch (GameManager.diasCompletos)
        {
            case 0:
                scriptDialogo.BindExternalFunction("AcertouPescador", Acertou);
                scriptDialogo.BindExternalFunction("ErrouPescador", Errou);
                break;

            case 1:
                scriptDialogo.BindExternalFunction("AcertouPescador2", Acertou);
                scriptDialogo.BindExternalFunction("ErrouPescador2", Errou);
                break;

            case 2:
                scriptDialogo.BindExternalFunction("AcertouPescador3", Acertou);
                scriptDialogo.BindExternalFunction("ErrouPescador3", Errou);
                break;
        }

        scriptDialogo.IsCanSkippingText = false;
        DialogBehaviour.instance.SetCharDelay(0.01f);
    }

    public void PescadorInteracao()
    {
        switch (GameManager.diasCompletos)
        {
            case 0:
                scriptDialogo.StartDialog(graficoNos[0]);
                break;

            case 1:
                scriptDialogo.StartDialog(graficoNos[1]);
                break;

            case 2:
                scriptDialogo.StartDialog(graficoNos[2]);
                break;
        }
        scriptMovimentacao.enabled = false;
        scriptCamera.enabled = false;
        analogico.SetActive(false);
        btnInteragir.SetActive(false);
    }

    void Acertou()
    {
        acertou = true;
        if (this != null)
            StartCoroutine(Mover());
    }

    void Errou()
    {
        acertou = false;
        if (this != null)
            StartCoroutine(Mover());
    }

    IEnumerator Mover()
    {
        yield return new WaitForSeconds(4f);

        GameObject.FindObjectOfType<Player_Pesquisador>().ReativarJogador();
        scriptMovimentacao.enabled = true;
        scriptCamera.enabled = true;
        analogico.SetActive(true);
        btnInteragir.SetActive(true);

        if (acertou)
        {
            QuestController.instance.AtualizarProgressoMissoes(missaoID, 1);
            acertou = false;
            GetComponent<DesaparecerNPCs>().Call_Desaparecer();
        }

    }
}