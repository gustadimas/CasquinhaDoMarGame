using UnityEngine;

public class Cam_Primeira_Pessoa : MonoBehaviour
{
    [SerializeField] Transform pesquisador;
    public Player_Pesquisador playerPesquisador;
    public float sensibilidade = 0.01f;
    public float suavidade = 1.5f;
    public float fatorEscala = 100f;

    Vector2 velocidade;
    Vector2 velocidadeFrame;

    void Reset() => pesquisador = GetComponentInParent<Player_Pesquisador>().transform;

    void Start()
    {
        Vector3 eulerAngles = transform.localRotation.eulerAngles;
        velocidade.y = -eulerAngles.x;
        velocidade.x = pesquisador.localRotation.eulerAngles.y;
    }

    void Update()
    {
        Vector2 _toqueDelta = Vector2.zero;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch _toque = Input.GetTouch(i);
            if (!RectTransformUtility.RectangleContainsScreenPoint(playerPesquisador.areaAnalogico, _toque.position))
            {
                _toqueDelta = _toque.deltaPosition / fatorEscala;
                break;
            }
        }

        Vector2 _velBrutaFrame = Vector2.Scale(_toqueDelta, Vector2.one * sensibilidade);
        velocidadeFrame = Vector2.Lerp(velocidadeFrame, _velBrutaFrame, 1 / suavidade);
        velocidade += velocidadeFrame;
        velocidade.y = Mathf.Clamp(velocidade.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-velocidade.y, Vector3.right);
        pesquisador.localRotation = Quaternion.AngleAxis(velocidade.x, Vector3.up);
    }
}
