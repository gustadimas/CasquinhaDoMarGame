using cherrydev;
using System.Collections;
using UnityEngine;

public class TuristaDialogoStarter : MonoBehaviour
{
    [SerializeField] DialogBehaviour scriptDialogo;
    [SerializeField] DialogNodeGraph[] graficoNos;
    [SerializeField] Player_Pesquisador scriptMovimentacao;
    [SerializeField] Cam_Primeira_Pessoa scriptCamera;
    [SerializeField] GameObject analogico, btnInteragir;
    [SerializeField] Transform posicaoJogador;
    [SerializeField] private int missaoID;

    bool acertou;
    private void Start()
    {
        scriptDialogo = GameObject.FindObjectOfType<DialogBehaviour>();
        scriptMovimentacao = GameObject.FindObjectOfType<Player_Pesquisador>();
        scriptCamera = GameObject.FindObjectOfType<Cam_Primeira_Pessoa>();
        analogico = GameObject.Find("Analogico");
        btnInteragir = GameObject.Find("Interface_Btn");
        posicaoJogador = transform.Find("Pesquisador");

        acertou = false;

        switch (QuestController.instance.diaAtual)
        {
            case 1:
                scriptDialogo.BindExternalFunction("AcertouTurista", Acertou);
                scriptDialogo.BindExternalFunction("ErrouTurista", Errou);
                break;

            case 2:
                scriptDialogo.BindExternalFunction("AcertouTurista2", Acertou);
                scriptDialogo.BindExternalFunction("ErrouTurista2", Errou);
                break;

            case 3:
                scriptDialogo.BindExternalFunction("AcertouTurista3", Acertou);
                scriptDialogo.BindExternalFunction("ErrouTurista3", Errou);
                break;
        }

        scriptDialogo.IsCanSkippingText = false;
        DialogBehaviour.instance.SetCharDelay(0.01f);
    }

    public void TuristaInteracao()
    {
        switch (QuestController.instance.diaAtual)
        {
            case 1:
                scriptDialogo.StartDialog(graficoNos[0]);
                break;

            case 2:
                scriptDialogo.StartDialog(graficoNos[1]);
                break;

            case 3:
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