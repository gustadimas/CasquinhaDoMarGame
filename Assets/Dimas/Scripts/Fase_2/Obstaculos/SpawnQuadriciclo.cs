using UnityEngine;
using System.Collections;

public class SpawnQuadriciclo : MonoBehaviour
{
    public GameObject quadricicloPrefab;
    public Transform[] pontosSpawn;
    public float intervaloSpawn = 5f;
    public float atrasoInicial = 20f;
    float proximoSpawn;
    [SerializeField] AudioSource somSpawn;
    [SerializeField] Transform filhote;
    bool primeiroSpawn = true;
    bool devePararSpawn = false;
    TutorialFase2 tutorial;

    private void Start()
    {
        tutorial = GetComponent<TutorialFase2>();
        StartCoroutine(AguardarAtrasoInicial());
    }

    IEnumerator AguardarAtrasoInicial()
    {
        yield return new WaitForSeconds(atrasoInicial);
        proximoSpawn = Time.time;
    }

    private void Update()
    {
        if (filhote != null && filhote.position.z >= 31)
            devePararSpawn = true;

        if (!devePararSpawn && Time.time >= proximoSpawn && !QuadricicloExiste() && GerenciadorNickname.instance.comecou)
        {
            SpawnarQuadriciclo();
            proximoSpawn = Time.time + intervaloSpawn;
        }
    }

    bool QuadricicloExiste()
    {
        return GameObject.FindObjectOfType<Quadriciclo>() != null;
    }

    void SpawnarQuadriciclo()
    {
        Transform _spawnPointEscolhido;

        if (primeiroSpawn)
        {
            _spawnPointEscolhido = pontosSpawn[3];
            tutorial.QuadricicloSpawnado();
        }
        else
            _spawnPointEscolhido = EscolherSpawnAleatorio();

        GameObject _quadriciclo = Instantiate(quadricicloPrefab, _spawnPointEscolhido.position, _spawnPointEscolhido.rotation);
        _quadriciclo.GetComponent<Quadriciclo>().SetarPontoSpawn(_spawnPointEscolhido.position);
        somSpawn.Play();

        primeiroSpawn = false;
    }

    Transform EscolherSpawnAleatorio()
    {
        int _spawnIndex = Random.Range(0, pontosSpawn.Length);
        return pontosSpawn[_spawnIndex];
    }
}