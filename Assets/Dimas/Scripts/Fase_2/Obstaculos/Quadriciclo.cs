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
    bool estaArrastando = false;
    bool jaFoi = false;

    public void SetSpawnPoint(Vector3 spawnPoint) => this.spawnPoint = spawnPoint;

    private void Start() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (!estaArrastando)
            MoverEmDirecaoAosFilhotes();

        VerificarDistanciaComFilhote();
    }

    private void VerificarDistanciaComFilhote()
    {
        filhotes = GameObject.FindGameObjectsWithTag("Filhote");
        if (filhotes.Length > 0)
        {
            GameObject _filhoteMaisProximo = ObterFilhoteMaisProximo();
            if (_filhoteMaisProximo != null)
            {
                float _distancia = Vector3.Distance(transform.position, _filhoteMaisProximo.transform.position);
                if (_distancia <= distanciaParaAcao && !jaFoi)
                {
                    jaFoi = true;
                    AcaoAoAlcancarFilhote();
                }
            }
        }
    }

    private void AcaoAoAlcancarFilhote()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        Collider _collider = GetComponent<Collider>();
        if (_collider != null)
            _collider.enabled = false;

        VidaTartarugas.instance.PerdeuVida();
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        tempoToqueComecou = Time.time;
        inicialPos = Input.mousePosition;
        estaArrastando = true;
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
            Vector2 _dragDirection = finalPos - inicialPos;
            Vector3 _direcaoSpawn = (spawnPoint - transform.position).normalized;

            Vector3 _dragDirectionWorld = Camera.main.ScreenToWorldPoint(new Vector3(_dragDirection.x, 0, 
                Camera.main.nearClipPlane)) - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            
            _dragDirectionWorld.Normalize();

            if (Vector3.Dot(_dragDirectionWorld, _direcaoSpawn) > 0)
            {
                rb.AddForce(_direcaoSpawn * forcaArrasteEmX, ForceMode.Impulse);
                Destroy(gameObject, 1f);
            }
        }

        estaArrastando = false;
    }

    private void MoverEmDirecaoAosFilhotes()
    {
        if (estaArrastando)
            return;

        filhotes = GameObject.FindGameObjectsWithTag("Filhote");
        if (filhotes.Length > 0)
        {
            GameObject _filhoteMaisProximo = ObterFilhoteMaisProximo();
            if (_filhoteMaisProximo != null)
            {
                Vector3 _direcao = (_filhoteMaisProximo.transform.position - transform.position).normalized;
                Vector3 _novaDirecao = new Vector3(_direcao.x, 0, _direcao.z);

                Quaternion novaRotacao = Quaternion.LookRotation(-_novaDirecao);

                transform.rotation = Quaternion.Slerp(transform.rotation, novaRotacao, Time.deltaTime * velocidade);

                transform.position += _novaDirecao * velocidade * Time.deltaTime;
            }
        }
    }

    private GameObject ObterFilhoteMaisProximo()
    {
        GameObject _filhoteMaisProximo = null;
        float menorDistancia = Mathf.Infinity;

        foreach (GameObject filhote in filhotes)
        {
            float distancia = Vector3.Distance(transform.position, filhote.transform.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                _filhoteMaisProximo = filhote;
            }
        }

        return _filhoteMaisProximo;
    }
}