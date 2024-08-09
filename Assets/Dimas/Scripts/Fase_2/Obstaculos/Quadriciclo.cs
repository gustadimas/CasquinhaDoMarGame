using UnityEngine;

public class Quadriciclo : MonoBehaviour
{
    Vector2 inicialPos, finalPos;
    float tempoToqueComecou, tempoToqueAcabou, intervaloTempo;

    [SerializeField] float forcaArrasteEmX = 50f;
    [SerializeField] float velocidade = 5f;
    [SerializeField] float distanciaMinimaArraste = 50f;
    [SerializeField] float distanciaParaAcao = 2f;

    Rigidbody rb;
    Vector3 spawnPoint;
    GameObject[] filhotes;
    bool isDragging = false;

    public void SetSpawnPoint(Vector3 spawnPoint) => this.spawnPoint = spawnPoint;

    private void Start() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (!isDragging)
            MoverEmDirecaoAosFilhotes();

        VerificarDistanciaComFilhote();
    }

    private void VerificarDistanciaComFilhote()
    {
        filhotes = GameObject.FindGameObjectsWithTag("Filhote");
        if (filhotes.Length > 0)
        {
            GameObject filhoteMaisProximo = ObterFilhoteMaisProximo();
            if (filhoteMaisProximo != null)
            {
                float distancia = Vector3.Distance(transform.position, filhoteMaisProximo.transform.position);
                if (distancia <= distanciaParaAcao && !jaFoi)
                {
                    jaFoi = true;
                    AcaoAoAlcancarFilhote();
                }
            }
        }
    }

    bool jaFoi = false;

    private void AcaoAoAlcancarFilhote()
    {
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        VidaTartarugas.instance.PerdeuVida();
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        tempoToqueComecou = Time.time;
        inicialPos = Input.mousePosition;
        isDragging = true;
        rb.isKinematic = true;
    }

    private void OnMouseDrag() => finalPos = Input.mousePosition;

    private void OnMouseUp()
    {
        tempoToqueAcabou = Time.time;
        intervaloTempo = tempoToqueAcabou - tempoToqueComecou;
        rb.isKinematic = false;

        if (Vector2.Distance(inicialPos, finalPos) >= distanciaMinimaArraste)
        {
            Vector2 dragDirection = finalPos - inicialPos;
            Vector3 direcaoSpawn = (spawnPoint - transform.position).normalized;

            Vector3 dragDirectionWorld = Camera.main.ScreenToWorldPoint(new Vector3(dragDirection.x, 0, Camera.main.nearClipPlane)) - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            dragDirectionWorld.Normalize();

            if (Vector3.Dot(dragDirectionWorld, direcaoSpawn) > 0)
            {
                rb.AddForce(direcaoSpawn * forcaArrasteEmX, ForceMode.Impulse);
                Destroy(gameObject, 1f);
            }
        }

        isDragging = false;
    }

    private void MoverEmDirecaoAosFilhotes()
    {
        if (isDragging)
            return;

        filhotes = GameObject.FindGameObjectsWithTag("Filhote");
        if (filhotes.Length > 0)
        {
            GameObject filhoteMaisProximo = ObterFilhoteMaisProximo();
            if (filhoteMaisProximo != null)
            {
                Vector3 direcao = (filhoteMaisProximo.transform.position - transform.position).normalized;
                Vector3 novaDirecao = new Vector3(direcao.x, 0, direcao.z);

                Quaternion novaRotacao = Quaternion.LookRotation(-novaDirecao);

                transform.rotation = Quaternion.Slerp(transform.rotation, novaRotacao, Time.deltaTime * velocidade);

                transform.position += novaDirecao * velocidade * Time.deltaTime;
            }
        }
    }

    private GameObject ObterFilhoteMaisProximo()
    {
        GameObject filhoteMaisProximo = null;
        float menorDistancia = Mathf.Infinity;

        foreach (GameObject filhote in filhotes)
        {
            float distancia = Vector3.Distance(transform.position, filhote.transform.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                filhoteMaisProximo = filhote;
            }
        }

        return filhoteMaisProximo;
    }
}
