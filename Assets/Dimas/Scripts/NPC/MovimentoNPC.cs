using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovimentoNPC : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    [SerializeField] float raioDeVagar = 15f;
    [SerializeField] float tempoEntreDestinos = 10f;
    [SerializeField] float tempoDeEspera = 1f;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Vagar());
    }

    private void Update() => AtualizarEstadoAnimacao();

    IEnumerator Vagar()
    {
        while (true)
        {
            Vector3 _destino = ObterDestinoAleatorio();
            agent.SetDestination(_destino);

            yield return new WaitUntil(() => agent.remainingDistance < 0.1f);

            yield return new WaitForSeconds(tempoDeEspera);

            tempoEntreDestinos = Random.Range(3f, 10f);
            yield return new WaitForSeconds(tempoEntreDestinos);
        }
    }

    private Vector3 ObterDestinoAleatorio()
    {
        NavMesh.SamplePosition(Random.insideUnitSphere * raioDeVagar + base.transform.position, out NavMeshHit hit, raioDeVagar, -1);
        return hit.position;
    }

    void AtualizarEstadoAnimacao()
    {
        Vector3 _movimento = agent.velocity;

        if (_movimento.magnitude > 0.1f)
            anim.SetInteger("estado", 1);
        else
            anim.SetInteger("estado", 0);
    }
}