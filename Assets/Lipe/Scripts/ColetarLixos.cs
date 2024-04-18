using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColetarLixos : MonoBehaviour
{
    public GameObject[] lixosRestantes;

    private void Update()
    {
        lixosRestantes = GameObject.FindGameObjectsWithTag("Lixo");
        if (EventController.eventoEmAndamento)
        {
            
            if (lixosRestantes.Length == 0)
            {
                GanhoXP.xp += 3;
                EventController.eventoEmAndamento = false;
                this.enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lixo"))
        {
            EventSpawner eventSpawner = FindObjectOfType<EventSpawner>();
            eventSpawner.RemoveObject(collision.gameObject);
            Destroy(collision.gameObject);

        }
    }
}