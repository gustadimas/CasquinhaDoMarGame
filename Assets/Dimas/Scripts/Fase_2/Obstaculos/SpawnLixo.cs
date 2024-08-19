using UnityEngine;
using System.Collections;

public class SpawnLixo : MonoBehaviour
{
    [SerializeField] GameObject prefabLixo;
    [SerializeField] Transform pontoSpawn;
    [SerializeField] float intervaloGeracao = 10f;
    [SerializeField] float limitePosicaoZ;
    [SerializeField] float atrasoSpawnLixo = 20f; // Atraso para o spawn de lixo
    TutorialFase2 tutorial; // Refer�ncia ao script de tutorial

    private bool primeiroSpawn = true;
    private float proximoSpawn;

    private void Start()
    {
        tutorial = GetComponent<TutorialFase2>();

        if (pontoSpawn != null)
            StartCoroutine(AguardarIntervaloInicial());
        else
            Debug.LogError("pontoSpawn n�o foi atribu�do!", this);
    }

    IEnumerator AguardarIntervaloInicial()
    {
        // Aguardar um tempo antes de iniciar o primeiro spawn
        yield return new WaitForSeconds(atrasoSpawnLixo);
        proximoSpawn = Time.time;
        VerificarEGerarLixo();
    }

    private void Update()
    {
        // Verificar se � hora de spawnar o pr�ximo lixo e se n�o h� nenhum lixo na cena
        if (Time.time >= proximoSpawn && FindObjectsOfType<Lixo>().Length == 0)
        {
            StartCoroutine(SpawnarComAtraso()); // Adicionar um atraso para o pr�ximo spawn
        }
    }

    IEnumerator SpawnarComAtraso()
    {
        // Adiciona um atraso antes de spawnar o lixo
        yield return new WaitForSeconds(atrasoSpawnLixo);
        VerificarEGerarLixo();
        proximoSpawn = Time.time + intervaloGeracao; // Ajustar o tempo do pr�ximo spawn
    }

    public void VerificarEGerarLixo()
    {
        // Verificar se o ponto de spawn est� configurado corretamente
        if (pontoSpawn == null || pontoSpawn.position.z >= limitePosicaoZ)
        {
            CancelInvoke(nameof(VerificarEGerarLixo));
            return;
        }

        // Verificar se n�o h� lixos na cena
        if (FindObjectsOfType<Lixo>().Length == 0)
        {
            GameObject lixo = Instantiate(prefabLixo, pontoSpawn.position, Quaternion.identity);
            if (primeiroSpawn)
            {
                tutorial.LixoTutorial(); // Iniciar tutorial no primeiro spawn de lixo
                primeiroSpawn = false;
            }
        }
    }
}
