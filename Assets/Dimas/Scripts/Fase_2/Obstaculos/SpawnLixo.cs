using UnityEngine;

public class SpawnLixo : MonoBehaviour
{
    [SerializeField] GameObject prefabLixo;
    [SerializeField] Transform pontoSpawn;
    [SerializeField] float intervaloGeracao = 10f;
    [SerializeField] float limitePosicaoZ;

    private void Start()
    {
        if (pontoSpawn != null)
        {
            InvokeRepeating(nameof(VerificarEGerarLixo), intervaloGeracao, intervaloGeracao);
        }
        else
        {
            Debug.LogError("pontoSpawn n�o foi atribu�do!", this);
        }
    }

    private void VerificarEGerarLixo()
    {
        if (pontoSpawn == null)
        {
            Debug.LogError("pontoSpawn � null. Verifique se foi atribu�do corretamente no Inspetor.", this);
            CancelInvoke(nameof(VerificarEGerarLixo));
            return;
        }

        if (pontoSpawn.position.z >= limitePosicaoZ)
        {
            CancelInvoke(nameof(VerificarEGerarLixo));
            return;
        }

        if (FindObjectsOfType<Lixo>().Length == 0)
            Instantiate(prefabLixo, pontoSpawn.position, Quaternion.identity);
    }
}