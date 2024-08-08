using UnityEngine;
using System.Collections;

public class SpawnLixo : MonoBehaviour
{
    [SerializeField] GameObject prefabLixo;
    [SerializeField] Transform pontoSpawn;
    [SerializeField] float intervaloGeracao = 10f;

    private void Start() => StartCoroutine(GerarLixoPeriodicamente());

    IEnumerator GerarLixoPeriodicamente()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloGeracao);

            if (FindObjectsOfType<Lixo>().Length == 0)
                Instantiate(prefabLixo, pontoSpawn.position, Quaternion.identity);
        }
    }
}
