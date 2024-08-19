using UnityEngine;
using System.Collections;

public class SpawnLixo : MonoBehaviour
{
    [SerializeField] GameObject prefabLixo;
    [SerializeField] Transform pontoSpawn;
    [SerializeField] float intervaloGeracao = 10f;
    [SerializeField] float limitePosicaoZ;
    [SerializeField] float atrasoInicialLixo;
    TutorialFase2 tutorial;

    bool primeiroSpawn = true;
    bool aguardandoAtraso = true;
    float proximoSpawn;

    private void Start()
    {
        tutorial = GetComponent<TutorialFase2>();

        if (pontoSpawn != null)
            StartCoroutine(AguardarAtrasoInicial());
        else
            Debug.LogError("pontoSpawn não foi atribuído!", this);
    }

    IEnumerator AguardarAtrasoInicial()
    {
        yield return new WaitForSeconds(atrasoInicialLixo);
        aguardandoAtraso = false;
        proximoSpawn = Time.time + intervaloGeracao;
    }

    private void Update()
    {
        if (aguardandoAtraso) return;

        if (Time.time >= proximoSpawn && FindObjectsOfType<Lixo>().Length == 0)
        {
            SpawnarLixo();
            proximoSpawn = Time.time + intervaloGeracao;
        }
    }

    void SpawnarLixo()
    {
        if (pontoSpawn == null || pontoSpawn.position.z >= limitePosicaoZ)
            return;

        Instantiate(prefabLixo, pontoSpawn.position, Quaternion.identity);

        if (primeiroSpawn)
        {
            tutorial.LixoTutorial();
            primeiroSpawn = false;
        }
    }
}