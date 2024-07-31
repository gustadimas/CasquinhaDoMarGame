using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CurarTartaruga : MonoBehaviour
{
    [SerializeField] float danoCurado;
    [SerializeField] Rigidbody rbTartaruga;
    IEnumerator CuraContinua()
    {
        float tempoAlimentacao = 3f;
        float intervaloEntreCuras = 0f;

        rbTartaruga.constraints = RigidbodyConstraints.FreezePosition;

        while (intervaloEntreCuras < tempoAlimentacao)
        {
            StatusTartaruga status = FindObjectOfType<StatusTartaruga>();
            status.RecuperarVida(danoCurado);

            yield return new WaitForSeconds(1);
            intervaloEntreCuras += 1f;
        }

        rbTartaruga.constraints = RigidbodyConstraints.None;
        rbTartaruga.constraints = RigidbodyConstraints.FreezeRotation;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tartaruga"))
        {
            StartCoroutine(CuraContinua());
        }
    }
}
