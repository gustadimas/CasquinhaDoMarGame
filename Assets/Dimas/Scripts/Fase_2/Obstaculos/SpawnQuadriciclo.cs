using UnityEngine;

public class SpawnQuadriciclo : MonoBehaviour
{
    public GameObject quadricicloPrefab;
    public Transform[] pontosSpawn;
    public float intervaloSpawn = 5f;
    [SerializeField] AudioSource somSpawn;
    float proximoSpawn;

    private void Update()
    {
        if (Time.time >= proximoSpawn && !QuadricicloExists() && GerenciadorNickname.instance.comecou)
        {
            SpawnarQuadriciclo();
            proximoSpawn = Time.time + intervaloSpawn;
        }
    }

    bool QuadricicloExists()
    {
        return GameObject.FindObjectOfType<Quadriciclo>() != null;
    }

    void SpawnarQuadriciclo()
    {
        Transform _spawnPointEscolhido = EscolherSpawnAleatorio();
        GameObject _quadriciclo = Instantiate(quadricicloPrefab, _spawnPointEscolhido.position, _spawnPointEscolhido.rotation);
        _quadriciclo.GetComponent<Quadriciclo>().SetarPontoSpawn(_spawnPointEscolhido.position);
        somSpawn.Play();
    }

    private Transform EscolherSpawnAleatorio()
    {
        int _spawnIndex = Random.Range(0, pontosSpawn.Length);
        return pontosSpawn[_spawnIndex];
    }
}