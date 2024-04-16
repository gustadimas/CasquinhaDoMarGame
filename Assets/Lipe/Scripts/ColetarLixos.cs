using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColetarLixos : MonoBehaviour
{
    public GameObject[] lixosRestantes;

    private void Update()
    {
        if (EventController.eventoEmAndamento)
        {
            lixosRestantes = GameObject.FindGameObjectsWithTag("Lixo");
            if (lixosRestantes.Length == 0)
            {
                EventController.eventoEmAndamento = false;
                this.enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lixo"))
        {
            Destroy(collision.gameObject);
        }
    }
}