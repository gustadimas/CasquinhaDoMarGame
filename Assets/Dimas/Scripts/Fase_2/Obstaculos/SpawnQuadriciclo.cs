using UnityEngine;

public class SpawnQuadriciclo : MonoBehaviour
{
    public GameObject quadricicloPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    [SerializeField] AudioSource spawnSound;
    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time >= nextSpawnTime && !QuadricicloExists() && GerenciadorNickname.instance.comecou)
        {
            SpawnarQuadriciclo();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private bool QuadricicloExists()
    {
        return GameObject.FindObjectOfType<Quadriciclo>() != null;
    }

    private void SpawnarQuadriciclo()
    {
        Transform spawnPointEscolhido = EscolherSpawnAleatorio();
        GameObject quadriciclo = Instantiate(quadricicloPrefab, spawnPointEscolhido.position, spawnPointEscolhido.rotation);
        quadriciclo.GetComponent<Quadriciclo>().SetSpawnPoint(spawnPointEscolhido.position);
        spawnSound.Play();
    }

    private Transform EscolherSpawnAleatorio()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[spawnIndex];
    }
}