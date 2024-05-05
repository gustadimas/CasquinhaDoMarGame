using cherrydev;
using UnityEngine;

public class PescadorDialogoStarter : MonoBehaviour
{
    [SerializeField] DialogBehaviour scriptDialogo;
    [SerializeField] DialogNodeGraph[] graficoNos;
    [SerializeField] Player_Pesquisador scriptMovimentacao;
    [SerializeField] Cam_Primeira_Pessoa scriptCamera;
    [SerializeField] GameObject analogico;
    [SerializeField] private int missaoID;



    void Start()
    {
        scriptDialogo = GameObject.FindObjectOfType<DialogBehaviour>();
        scriptMovimentacao = GameObject.FindObjectOfType<Player_Pesquisador>();
        scriptCamera = GameObject.FindObjectOfType<Cam_Primeira_Pessoa>();
        analogico = GameObject.Find("Analogico");

        switch (QuestController.instance.diaAtual)
        {
            case 1:
                scriptDialogo.BindExternalFunction("AcabouPescador", ReativarPersonagem);
                break;

            case 2:
                scriptDialogo.BindExternalFunction("AcabouPescador2", ReativarPersonagem);
                break;

            case 3:
                scriptDialogo.BindExternalFunction("AcabouPescador3", ReativarPersonagem);
                break;
        }
    } 

    public void PescadorInteracao()
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
    }

    void ReativarPersonagem()
    {
        GameObject.FindObjectOfType<Player_Pesquisador>().ReativarJogador();
        Invoke(nameof(Mover), 1.5f);
    }

    void Mover()
    {
        scriptMovimentacao.enabled = true;
        scriptCamera.enabled = true;
        analogico.SetActive(true);
        Destroy(transform.parent.gameObject);
        QuestController.instance.AtualizarProgressoMissoes(missaoID, 1);
    }
}
