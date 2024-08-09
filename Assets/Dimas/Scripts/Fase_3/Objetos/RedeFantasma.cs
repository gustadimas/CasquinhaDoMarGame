using UnityEngine;
using System.Collections;

public class RedeFantasma : MonoBehaviour
{
    [SerializeField] Tartaruga scriptMovimentacao;
    [SerializeField] GameObject analogicoMov;
    [SerializeField] GameObject analogicoRot;
    [SerializeField] Transform posicaoTartaruga;

    [SerializeField] float danoCausado;
    [SerializeField] float tempoEntreDanos = 2f;
    bool podeAplicarDano = true;

    MeshRenderer mesh;
    Rigidbody rbTartaruga;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();

        scriptMovimentacao = GameObject.FindObjectOfType<Tartaruga>();
        if (scriptMovimentacao != null)
        {
            posicaoTartaruga = scriptMovimentacao.transform;
            rbTartaruga = scriptMovimentacao.GetComponent<Rigidbody>();
        }

        analogicoMov = GameObject.Find("AnalogicoMov");
        analogicoRot = GameObject.Find("AnalogicoRot");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tartaruga") && podeAplicarDano)
        {
            StatusTartaruga _status = collision.gameObject.GetComponent<StatusTartaruga>();
            _status.ReceberDano(danoCausado);

            if (rbTartaruga != null)
            {
                StartCoroutine(DesativarTemporariamente(rbTartaruga));
            }

            StartCoroutine(Mover());

            podeAplicarDano = false;
            StartCoroutine(TempoDeEsperaEntreDanos());
        }
    }

    private IEnumerator TempoDeEsperaEntreDanos()
    {
        yield return new WaitForSeconds(tempoEntreDanos);
        podeAplicarDano = true;
    }

    IEnumerator DesativarTemporariamente(Rigidbody rb)
    {
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.1f);
        rb.isKinematic = false;
    }

    IEnumerator Mover()
    {
        scriptMovimentacao.enabled = false;
        analogicoMov.SetActive(false);
        analogicoRot.SetActive(false);

        yield return new WaitForSeconds(4f);
        mesh.enabled = false;

        scriptMovimentacao.ReativarJogador();
        scriptMovimentacao.enabled = true;
        analogicoMov.SetActive(true);
        analogicoRot.SetActive(true);
        Destroy(gameObject);
    }
}