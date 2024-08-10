using System.Collections;
using UnityEngine;

public class CurarTartaruga : MonoBehaviour
{
    [SerializeField] float danoCurado;
    [SerializeField] Tartaruga scriptMovimentacao;
    [SerializeField] GameObject analogicoMov;
    [SerializeField] GameObject analogicoRot;
    Rigidbody rbTartaruga;

    private void Start()
    {
        scriptMovimentacao = GameObject.FindObjectOfType<Tartaruga>();
        if (scriptMovimentacao != null)
        {
            rbTartaruga = scriptMovimentacao.GetComponent<Rigidbody>();
        }

        analogicoMov = GameObject.Find("AnalogicoMov");
        analogicoRot = GameObject.Find("AnalogicoRot");
    }

    IEnumerator CuraContinua()
    {
        float _tempoAlimentacao = 3f;
        float _intervaloEntreCuras = 0f;

        scriptMovimentacao.enabled = false;
        analogicoMov.SetActive(false);
        analogicoRot.SetActive(false);

        if (rbTartaruga != null)
        {
            StartCoroutine(DesativarTemporariamente(rbTartaruga));
        }

        while (_intervaloEntreCuras < _tempoAlimentacao)
        {
            StatusTartaruga status = FindObjectOfType<StatusTartaruga>();
            status.RecuperarVida(danoCurado);

            yield return new WaitForSeconds(1);
            _intervaloEntreCuras += 1f;
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

    IEnumerator DesativarTemporariamente(Rigidbody rb)
    {
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.1f);
        rb.isKinematic = false;
    }
}