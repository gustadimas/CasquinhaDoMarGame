using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    EventController eventController;
    [SerializeField] GameObject lixos, turistas, pescadores;
    MonoBehaviour scriptColetaLixo;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private float minDistance = 2f;

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
                GameObject lixoObj = Instantiate(lixos, GetSpawnPosition(), Quaternion.identity);
                spawnedObjects.Add(lixoObj);
            }
            scriptColetaLixo.enabled = true;
        }

        if (randomizedEvent == "EventoTurista")
        {
            EventController.eventoEmAndamento = true;
            GameObject turistaObj = Instantiate(turistas, GetSpawnPosition(), Quaternion.identity);
            spawnedObjects.Add(turistaObj);
            scriptColetaLixo.enabled = true;
        }

        if (randomizedEvent == "EventoPescador")
        {
            EventController.eventoEmAndamento = true;
            GameObject pescadorObj = Instantiate(pescadores, GetSpawnPosition(), Quaternion.identity);
            spawnedObjects.Add(pescadorObj);
            scriptColetaLixo.enabled = true;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition;
        bool positionFound = false;

        while (!positionFound)
        {
            spawnPosition = new Vector3(UnityEngine.Random.Range(-5, 5), 4, -3);
            positionFound = true;

            foreach (GameObject obj in spawnedObjects)
            {
                if (Vector3.Distance(spawnPosition, obj.transform.position) < minDistance)
                {
                    positionFound = false;
                    break;
                }
            }

            if (positionFound)
            {
                return spawnPosition;
            }
        }

        return Vector3.zero;
    }

    public void RemoveObject(GameObject spawnedObject)
    {
        spawnedObjects.Remove(spawnedObject);
    }
}
