using UnityEngine;

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
            Debug.Log("bateuu");
            Destroy(collision.gameObject);
            VidaTartarugas.instance.PerdeuVida();
        }
    }
}