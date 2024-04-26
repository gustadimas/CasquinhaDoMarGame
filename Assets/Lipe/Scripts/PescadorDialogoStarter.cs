using cherrydev;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PescadorDialogoStarter : MonoBehaviour
{
    [SerializeField] DialogBehaviour scriptDialogo;
    [SerializeField] DialogNodeGraph graficoNos;
    [SerializeField] MonoBehaviour scriptMovimentacao;
    [SerializeField] GameObject analogico;
    [SerializeField] private int missaoID;

    void Start() => scriptDialogo.BindExternalFunction("AcabouPescador", ReativarPersonagem);
    
    public void PescadorInteracao()
    {
        scriptDialogo.StartDialog(graficoNos);
        scriptMovimentacao.enabled = false;
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
        analogico.SetActive(true);
        GameObject.FindObjectOfType<EventSpawner>().SumirNPCs();
        FindAnyObjectByType<QuestController>().AtualizarProgressoMissoes(missaoID, 1);
    }
}
