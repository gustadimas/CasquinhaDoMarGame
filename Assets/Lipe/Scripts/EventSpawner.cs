using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    EventController eventController;
    [SerializeField] GameObject lixoPrefab;
    [SerializeField] GameObject turista, pescador;
    GameObject pesquisador;

    public List<GameObject> spawnedObjects = new List<GameObject>();
    float minDistance = 2f;
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
            switch (QuestController.instance.diaAtual)
            {
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject lixoObj = Instantiate(lixoPrefab, GetSpawnPosition(), Quaternion.identity);
                        spawnedObjects.Add(lixoObj);
                    }
                    break;

                case 2:
                    for (int i = 0; i < 10; i++)
                    {
                        GameObject lixoObj = Instantiate(lixoPrefab, GetSpawnPosition(), Quaternion.identity);
                        spawnedObjects.Add(lixoObj);
                    }
                    break;

                case 3:
                    for (int i = 0; i < 20; i++)
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
            Instantiate(turista, GetSpawnPosition(), Quaternion.identity);

        }

        if (randomizedEvent == "EventoPescador")
        {
            EventController.eventoEmAndamento = true;
            Instantiate(pescador, GetSpawnPosition(), Quaternion.identity);
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
