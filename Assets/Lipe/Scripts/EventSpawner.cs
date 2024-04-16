using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    EventController eventController;
    [SerializeField] GameObject lixos, turistas, pescadores;
    MonoBehaviour scriptColetaLixo;

    // Start is called before the first frame update
    void Start()
    {
        eventController = FindObjectOfType<EventController>();
        eventController.OnRandomizedEvent += EventController_OnRandomizedEvent;
        scriptColetaLixo = FindAnyObjectByType<ColetarLixos>();
        scriptColetaLixo.enabled = false;
    }

    private void EventController_OnRandomizedEvent(string randomizedEvent)
    {
        if (randomizedEvent == "EventoLixo")
        {
            EventController.eventoEmAndamento = true;
            for (int i = 0; i < 3; i++)
            {
                Instantiate(lixos, new Vector3(UnityEngine.Random.Range(-5, 5), 4, -3), Quaternion.identity);
                
            }
            scriptColetaLixo.enabled = true;
        }

        if (randomizedEvent == "EventoTurista")
        {
            EventController.eventoEmAndamento = true;
            Instantiate(turistas, new Vector3(-8, 5, -11), Quaternion.identity);
        }

        if (randomizedEvent == "EventoPescador")
        {
            EventController.eventoEmAndamento = true;
            Instantiate(pescadores, new Vector3(-4, 5, -11), Quaternion.identity);
        }
    }

}