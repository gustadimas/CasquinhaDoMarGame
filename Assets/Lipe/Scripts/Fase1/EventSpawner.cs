using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    EventController eventController;
    [SerializeField] GameObject lixoPrefab;
    [SerializeField] GameObject turista, pescador;
    GameObject pesquisador;

    public List<GameObject> spawnedObjects = new List<GameObject>();

    [SerializeField] GameObject[] pointsPescador, pointsTurista;

    float minDistance = 10f;

    void Start()
    {
        eventController = FindObjectOfType<EventController>();
        eventController.OnRandomizedEvent += EventController_OnRandomizedEvent;

        pesquisador = GameObject.FindGameObjectWithTag("Player");
    }

    private void EventController_OnRandomizedEvent(string randomizedEvent)
    {
        if (randomizedEvent == "EventoLixo")
        {
            EventController.eventoEmAndamento = true;
            switch (GameManager.diasCompletos)
            {
                case 0:
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject lixoObj = Instantiate(lixoPrefab, GetSpawnPosition(), Quaternion.identity);
                        spawnedObjects.Add(lixoObj);
                    }
                    break;

                case 1:
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject lixoObj = Instantiate(lixoPrefab, GetSpawnPosition(), Quaternion.identity);
                        spawnedObjects.Add(lixoObj);
                    }
                    break;

                case 2:
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject lixoObj = Instantiate(lixoPrefab, GetSpawnPosition(), Quaternion.identity);
                        spawnedObjects.Add(lixoObj);
                    }
                    break;
            }
            
        }

        if (randomizedEvent == "EventoTurista")
        {
            EventController.eventoEmAndamento = true;
            switch (GameManager.diasCompletos)
            {
                case 0:
                    Instantiate(turista, pointsTurista[0].transform.position, Quaternion.identity);
                    break;

                case 1:
                    Instantiate(turista, pointsTurista[1].transform.position, Quaternion.identity);
                    break;

                case 2:
                    Instantiate(turista, pointsTurista[2].transform.position, Quaternion.identity);
                    break;
            }
            
            

        }

        if (randomizedEvent == "EventoPescador")
        {
            EventController.eventoEmAndamento = true;
            switch (GameManager.diasCompletos)
            {
                case 0:
                    Instantiate(pescador, pointsPescador[0].transform.position, Quaternion.identity);
                    break;

                case 1:
                    Instantiate(pescador, pointsPescador[1].transform.position, Quaternion.identity);
                    break;

                case 2:
                    Instantiate(pescador, pointsPescador[2].transform.position, Quaternion.identity);
                    break;
            }
            
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition;
        bool positionFound = false;

        while (!positionFound)
        {
            spawnPosition = new Vector3(UnityEngine.Random.Range(-19, 19), -1, UnityEngine.Random.Range(-23, -6));
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
