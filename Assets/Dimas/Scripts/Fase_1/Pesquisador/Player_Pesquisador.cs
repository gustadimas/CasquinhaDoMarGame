using UnityEngine;

public class Player_Pesquisador : MonoBehaviour
{
    [Header("Configuracoes:")]
    [SerializeField] public bool desativarEntradas = false;
    [SerializeField] float velocidadeMovimento;

    [Header("Atribuicoes:")]
    [SerializeField] RectTransform analogico;
    public RectTransform areaAnalogico;
    [SerializeField] RectTransform pontoAnalogico;
    [SerializeField] Transform cameraTransform;

    Vector3 direcaoMovimento;
    Vector2 posicaoPonto, posicaoInicialPonto;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posicaoInicialPonto = pontoAnalogico.position;
    }

    void Update()
    {
        if (!desativarEntradas)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch _toque = Input.GetTouch(i);
                Vector2 _posicaoToque = _toque.position;

                if (RectTransformUtility.RectangleContainsScreenPoint(areaAnalogico, _posicaoToque))
                {
                    if (_toque.phase == TouchPhase.Moved || _toque.phase == TouchPhase.Stationary)
                    {
                        posicaoPonto = _toque.position - (Vector2)analogico.position;
                        posicaoPonto = Vector2.ClampMagnitude(posicaoPonto, 120f);

                        pontoAnalogico.position = (Vector2)analogico.position + posicaoPonto;

                        direcaoMovimento = new Vector3(posicaoPonto.x, 0, posicaoPonto.y).normalized;
                    }

                    if (_toque.phase == TouchPhase.Ended || _toque.phase == TouchPhase.Canceled)
                    {
                        ResetarPosicaoJoystick();
                        direcaoMovimento = Vector3.zero;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (direcaoMovimento != Vector3.zero)
        {
            Vector3 movimento = transform.TransformDirection(direcaoMovimento);
            rb.MovePosition(transform.position + velocidadeMovimento * Time.deltaTime * movimento);
        }
    }

    void ResetarPosicaoJoystick()
    {
        posicaoPonto = Vector2.zero;
        pontoAnalogico.position = posicaoInicialPonto;
    }

    public void ReativarJogador()
    {
        direcaoMovimento = Vector3.zero;
        ResetarPosicaoJoystick();
    }
}