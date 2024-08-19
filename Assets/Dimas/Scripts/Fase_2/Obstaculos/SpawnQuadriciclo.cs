using UnityEngine;
using System.Collections;
public class SpawnQuadriciclo : MonoBehaviour
{
    public GameObject quadricicloPrefab;
    public Transform[] pontosSpawn;
    public float intervaloSpawn = 5f;
    public float atrasoInicial = 20f;
    [SerializeField] AudioSource somSpawn;
    private float proximoSpawn;

    private bool primeiroSpawn = true;
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

    private Transform EscolherSpawnAleatorio()
    {
        int _spawnIndex = Random.Range(0, pontosSpawn.Length);
        return pontosSpawn[_spawnIndex];
    }
}