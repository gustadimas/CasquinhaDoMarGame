using UnityEngine;
using UnityEngine.SceneManagement;

public class Filhotes : MonoBehaviour
{
    public Transform destino;
    public float velocidade = 2f;

    private void Update()
    {
        Vector3 direcao = (destino.position - transform.position).normalized;
        transform.position += direcao * velocidade * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lixo"))
        {
            Destroy(other.gameObject);
            VidaTartarugas.instance.PerdeuVida();
        }

        if (other.gameObject.CompareTag("DestinoFilhote"))
            FadeManager.instance.CarregarProximaCenaComFade();
    }
}