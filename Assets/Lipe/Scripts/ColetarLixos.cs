using UnityEngine;

public class ColetarLixos : MonoBehaviour
{
    EventSpawner eventSpawner;

    private void Start()
    {
        eventSpawner = FindObjectOfType<EventSpawner>();
    }

    private void Update()
    {
        if (EventController.eventoEmAndamento)
        {

            if (eventSpawner.spawnedObjects.Count == 0)
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
            eventSpawner.RemoveObject(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}