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

    void Update()
    {
        Vector2 toqueDelta = Vector2.zero;

        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (!RectTransformUtility.RectangleContainsScreenPoint(playerPesquisador.areaAnalogico, toque.position))
                toqueDelta = toque.deltaPosition / fatorEscala;
        }

        Vector2 rawFrameVelocity = Vector2.Scale(toqueDelta, Vector2.one * sensibilidade);
        velocidadeFrame = Vector2.Lerp(velocidadeFrame, rawFrameVelocity, 1 / suavidade);
        velocidade += velocidadeFrame;
        velocidade.y = Mathf.Clamp(velocidade.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-velocidade.y, Vector3.right);
        pesquisador.localRotation = Quaternion.AngleAxis(velocidade.x, Vector3.up);
    }
}