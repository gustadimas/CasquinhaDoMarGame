using System.Collections;
using UnityEngine;

public class CurarTartaruga : MonoBehaviour
{
    [SerializeField] float danoCurado;
    [SerializeField] Tartaruga scriptMovimentacao;
    [SerializeField] GameObject analogicoMov;
    [SerializeField] GameObject analogicoRot;
    IEnumerator CuraContinua()
    {
        float tempoAlimentacao = 3f;
        float intervaloEntreCuras = 0f;

        scriptMovimentacao.enabled = false;
        analogicoMov.SetActive(false);
        analogicoRot.SetActive(false);

        while (intervaloEntreCuras < tempoAlimentacao)
        {
            StatusTartaruga status = FindObjectOfType<StatusTartaruga>();
            status.RecuperarVida(danoCurado);

            yield return new WaitForSeconds(1);
            intervaloEntreCuras += 1f;
        }

        scriptMovimentacao.ReativarJogador();
        scriptMovimentacao.enabled = true;
        analogicoMov.SetActive(true);
        analogicoRot.SetActive(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tartaruga"))
        {
            if (StatusTartaruga.instance.vidaAtual != 1 && StatusTartaruga.instance.podeCurar)
                StartCoroutine(CuraContinua());
        }
    }
}