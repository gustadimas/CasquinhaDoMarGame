using System.Collections;
using UnityEngine;

public class RedeFantasma : MonoBehaviour
{
    [SerializeField] Tartaruga scriptMovimentacao;
    [SerializeField] GameObject analogicoMov;
    [SerializeField] GameObject analogicoRot;
    [SerializeField] Transform posicaoTartaruga;

    [SerializeField] float danoCausado;

    MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();

        scriptMovimentacao = GameObject.FindObjectOfType<Tartaruga>();
        if (scriptMovimentacao != null)
        {
            posicaoTartaruga = scriptMovimentacao.transform;
        }

        analogicoMov = GameObject.Find("AnalogicoMov");
        analogicoRot = GameObject.Find("AnalogicoRot");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tartaruga"))
        {
            StatusTartaruga status = FindObjectOfType<StatusTartaruga>();
            status.ReceberDano(danoCausado);
            StartCoroutine(Mover());
        }
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
