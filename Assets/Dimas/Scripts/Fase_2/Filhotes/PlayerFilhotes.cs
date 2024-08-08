using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerFilhotes : MonoBehaviour
{
    public Transform destino;
    public float velocidade = 2f;

    private void Update()
    {
        Vector3 direcao = (destino.position - transform.position).normalized;
        transform.position += direcao * velocidade * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Quadriciclo"))
        {
            Destroy(collision.gameObject);
            VidaTartarugas.instance.PerdeuVida();
        }

        if (collision.gameObject.CompareTag("DestinoFilhote"))
            CarregarProximaCena();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lixo"))
        {
            Destroy(collision.gameObject);
            VidaTartarugas.instance.PerdeuVida();
        }
    }

    private void CarregarProximaCena()
    {
        int cenaAtual = SceneManager.GetActiveScene().buildIndex;
        int proximaCena = GameManager.proximaEtapa;

        Debug.Log($"Cena Atual: {cenaAtual}, Próxima Cena: {proximaCena}");

        if (proximaCena <= cenaAtual)
        {
            proximaCena = cenaAtual + 1;
            GameManager.proximaEtapa = proximaCena;
        }

        if (proximaCena < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(proximaCena);
        else
            Debug.LogError("Proxima cena está fora do range das cenas configuradas no Build Settings.");
    }
}