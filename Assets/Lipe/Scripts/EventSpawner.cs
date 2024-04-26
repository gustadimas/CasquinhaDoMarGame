using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    EventController eventController;
    [SerializeField] GameObject lixoPrefab;
    GameObject turista, pescador, pesquisador;

    public List<GameObject> spawnedObjects = new List<GameObject>();
    private float minDistance = 2f;

    void Start()
    {
        eventController = FindObjectOfType<EventController>();
        eventController.OnRandomizedEvent += EventController_OnRandomizedEvent;

        pesquisador = GameObject.FindGameObjectWithTag("Player");
        turista = GameObject.FindGameObjectWithTag("Turista");
        pescador = GameObject.FindGameObjectWithTag("Pescador");

        SumirNPCs();
    }

    public void SumirNPCs()
    {
        turista.SetActive(false);
        pescador.SetActive(false);
    }

    private void EventController_OnRandomizedEvent(string randomizedEvent)
    {
        if (randomizedEvent == "EventoLixo")
        {
            EventController.eventoEmAndamento = true;
            for (int i = 0; i < 3; i++)
            {
                GameObject lixoObj = Instantiate(lixoPrefab, GetSpawnPosition(), Quaternion.identity);
                spawnedObjects.Add(lixoObj);
            }
        }

        if (randomizedEvent == "EventoTurista")
        {
            EventController.eventoEmAndamento = true;
            turista.transform.position = GetSpawnPosition();
            turista.SetActive(true);
        }

        if (randomizedEvent == "EventoPescador")
        {
            EventController.eventoEmAndamento = true;
            pescador.transform.position = GetSpawnPosition();
            pescador.SetActive(true);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition;
        bool positionFound = false;

        while (!positionFound)
        {
            spawnPosition = new Vector3(UnityEngine.Random.Range(-11, 5), 1, UnityEngine.Random.Range(-11, -3));
            positionFound = true;

            foreach (GameObject obj in spawnedObjects)
            {
                if (Vector3.Distance(spawnPosition, obj.transform.position) < minDistance || Vector3.Distance(spawnPosition, pesquisador.transform.position) < minDistance)
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
