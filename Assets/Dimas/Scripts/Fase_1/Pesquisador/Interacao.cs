using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Interacao : MonoBehaviour
{
    [SerializeField] private TipoInteracao tipoInteracao;
    [SerializeField] private float distanciaRaio = 3f;

    [Header("Missoes")]
    protected QuestController controladorMissao;

    private Camera mainCamera;
    private Ray raio;
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

                    hitInfo.collider.GetComponent<Collider>().enabled = false;
                    if (hitInfo.collider.GetComponentInParent<Outline>() != null)
                    {
                        hitInfo.collider.GetComponentInParent<Outline>().enabled = false;
                    }
                    Destroy(hitInfo.collider.transform.parent.gameObject);
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