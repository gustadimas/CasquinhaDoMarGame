using cherrydev;
using UnityEngine;

public class PescadorDialogoStarter : MonoBehaviour
{
    [SerializeField] DialogBehaviour scriptDialogo;
    [SerializeField] DialogNodeGraph graficoNos;
    [SerializeField] MonoBehaviour scriptMovimentacao, scriptCamera;
    [SerializeField] GameObject analogico;
    [SerializeField] private int missaoID;

    void Start() => scriptDialogo.BindExternalFunction("AcabouPescador", ReativarPersonagem);

    public void PescadorInteracao()
    {
        scriptDialogo.StartDialog(graficoNos);
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
        GameObject.FindObjectOfType<EventSpawner>().SumirNPCs();
        QuestController.instance.AtualizarProgressoMissoes(missaoID, 1);
    }
}
