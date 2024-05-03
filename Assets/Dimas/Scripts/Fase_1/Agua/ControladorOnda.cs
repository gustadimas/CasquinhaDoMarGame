using UnityEngine;

public class ControladorOnda : MonoBehaviour
{
    public static ControladorOnda instance;

    [SerializeField] float amplitude;
    [SerializeField] float tamanho;
    [SerializeField] float velocidade;
    [SerializeField] float desvio;

    private void Awake()
    {
        if (instance == null) instance = this;

        else if (instance == this)
        {
            print("Instancia do objeto ja existe, destruindo objeto!");
            Destroy(this);
        }
    }

    private void Update()
    {
        desvio += Time.deltaTime * velocidade;
    }

    public float AlturaOnda(float _x)
    {
        return amplitude * Mathf.Sin(_x / tamanho + desvio);
    }
}
