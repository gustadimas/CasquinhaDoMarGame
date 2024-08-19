using System.Collections;
using UnityEngine;

public class CurarTartaruga : MonoBehaviour
{
    [SerializeField] float danoCurado = 0.1f;
    [SerializeField] Tartaruga scriptMovimentacao;
    [SerializeField] GameObject analogicoMov;
    [SerializeField] GameObject analogicoRot;
    Rigidbody rbTartaruga;

    bool estaCurando = false;

    private void Start()
    {
        scriptMovimentacao = GameObject.FindObjectOfType<Tartaruga>();

        if (scriptMovimentacao != null)
            rbTartaruga = scriptMovimentacao.GetComponent<Rigidbody>();

        analogicoMov = GameObject.Find("AnalogicoMov");
        analogicoRot = GameObject.Find("AnalogicoRot");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tartaruga") && !estaCurando)
        {
            if (StatusTartaruga.instance.vidaAtual < 1f && StatusTartaruga.instance.podeCurar)
                StartCoroutine(CuraContinua());
        }
    }

    IEnumerator CuraContinua()
    {
        estaCurando = true;

        scriptMovimentacao.enabled = false;
        analogicoMov.SetActive(false);
        analogicoRot.SetActive(false);

        if (rbTartaruga != null)
            StartCoroutine(DesativarTemporariamente(rbTartaruga));

        float _tempoAlimentacao = 3f;
        float _intervaloEntreCuras = 0f;
        float _tempoEsperaCura = 1f;

        StatusTartaruga _status = StatusTartaruga.instance;

        while (_intervaloEntreCuras < _tempoAlimentacao && _status.vidaAtual < 1f)
        {
            _status.RecuperarVida(danoCurado);

            if (_status.vidaAtual >= 1f)
            {
                _status.vidaAtual = 1f;
                break;
            }

            yield return new WaitForSeconds(_tempoEsperaCura);
            _intervaloEntreCuras += _tempoEsperaCura;
        }

        scriptMovimentacao.ReativarJogador();
        scriptMovimentacao.enabled = true;
        analogicoMov.SetActive(true);
        analogicoRot.SetActive(true);

        estaCurando = false;
        Destroy(gameObject);
    }

    IEnumerator DesativarTemporariamente(Rigidbody rb)
    {
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.1f);
        rb.isKinematic = false;
    }
}