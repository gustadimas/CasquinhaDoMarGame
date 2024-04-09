using cherrydev;
using Unity.VisualScripting;
using UnityEngine;

public class TuristaDialogoStarter : MonoBehaviour
{
    [SerializeField] DialogBehaviour scriptDialogo;
    [SerializeField] DialogNodeGraph graficoNos;
    [SerializeField] MonoBehaviour scriptMovimentacao;
    [SerializeField] GameObject analogico;
    void Start()
    {
        scriptDialogo.BindExternalFunction("AcabouTurista", ReativarPersonagem);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            scriptDialogo.StartDialog(graficoNos);
            scriptMovimentacao.enabled = false;
            analogico.SetActive(false);
        }
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
    }
}