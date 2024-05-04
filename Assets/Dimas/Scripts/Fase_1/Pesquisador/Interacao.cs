using UnityEngine;

public class Interacao : MonoBehaviour
{
    [SerializeField] TipoInteracao tipoInteracao;
    [SerializeField] float distanciaRaio = 5f;

    [Header("Missoes")]
    protected QuestController controladorMissao;

    Camera mainCamera;
    Ray raio;
    protected RaycastHit hitInfo;
    GameObject ultimoObjetoDestacado;

    public TipoInteracao TipoInteracao => tipoInteracao;

    private void Awake()
    {
        mainCamera = Camera.main;
        controladorMissao = FindAnyObjectByType<QuestController>();
    }

    private void Update()
    {
        InteracaoRaycast();

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.TryGetComponent(out Lixos _lixo))
            {
                tipoInteracao = TipoInteracao.lixo;
                hitInfo.collider.GetComponentInParent<Outline>().enabled = true;
                ultimoObjetoDestacado = hitInfo.collider.gameObject;
            }

            else if (hitInfo.collider.TryGetComponent(out PescadorDialogoStarter _pescador))
            {
                tipoInteracao = TipoInteracao.pescador;
                hitInfo.collider.GetComponentInParent<Outline>().enabled = true;
                ultimoObjetoDestacado = hitInfo.collider.gameObject;
            }

            else if (hitInfo.collider.TryGetComponent(out TuristaDialogoStarter _turista))
            {
                tipoInteracao = TipoInteracao.turista;
                hitInfo.collider.GetComponentInParent<Outline>().enabled = true;
                ultimoObjetoDestacado = hitInfo.collider.gameObject;
            }

            else tipoInteracao = TipoInteracao.None;
        }
        else
        {
            tipoInteracao = TipoInteracao.None;
            if (ultimoObjetoDestacado != null)
            {
                ultimoObjetoDestacado.GetComponentInParent<Outline>().enabled = false;
                ultimoObjetoDestacado = null;
            }
        }
    }
    public void InteragirLixo()
    {
        if (tipoInteracao == TipoInteracao.lixo)
        {
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.TryGetComponent(out Lixos _lixo))
                {
                    controladorMissao.AtualizarProgressoMissoes(_lixo.MissaoID, 1);
                    FindAnyObjectByType<EventSpawner>().RemoveObject(hitInfo.collider.transform.parent.gameObject);

                    hitInfo.collider.GetComponent<Collider>().enabled = false;
                    if (hitInfo.collider.GetComponentInParent<Outline>() != null)
                        hitInfo.collider.GetComponentInParent<Outline>().enabled = false;

                    Destroy(hitInfo.collider.transform.parent.gameObject);
                }
            }
        }
    }

    public void InteragirPescador()
    {
        if (tipoInteracao == TipoInteracao.pescador)
        {
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.TryGetComponent(out PescadorDialogoStarter _pescador))
                {
                    _pescador.PescadorInteracao();
                    if (hitInfo.collider.GetComponentInParent<Outline>() != null)
                        hitInfo.collider.GetComponentInParent<Outline>().enabled = false;
                }
            }
        }
    }

    public void InteragirTurista()
    {
        if (tipoInteracao == TipoInteracao.turista)
        {
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.TryGetComponent(out TuristaDialogoStarter _turista))
                {
                    _turista.TuristaInteracao();
                    if (hitInfo.collider.GetComponentInParent<Outline>() != null)
                        hitInfo.collider.GetComponentInParent<Outline>().enabled = false;
                }
            }
        }
    }

    private void InteracaoRaycast()
    {
        raio = mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        Physics.Raycast(raio, out hitInfo, distanciaRaio);
        Debug.DrawRay(raio.origin, raio.direction * distanciaRaio, color: Color.red);
    }
}