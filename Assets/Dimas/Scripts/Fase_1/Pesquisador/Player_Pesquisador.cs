using UnityEngine;

public class Player_Pesquisador : MonoBehaviour
{
    [Header("Configuracoes:")]
    [SerializeField] bool desativarEntradas = false;
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
        if (!desativarEntradas) Joystick();
    }

    void FixedUpdate()
    {
        if (direcaoMovimento != Vector3.zero)
        {
            Vector3 _movimentoHorizontal = cameraTransform.TransformDirection(new Vector3(direcaoMovimento.x, 0, direcaoMovimento.y));
            rb.MovePosition(transform.position +  velocidadeMovimento * Time.deltaTime * _movimentoHorizontal);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_movimentoHorizontal), 0.15f);
        }
    }

    void Joystick()
    {
        if (Input.touchCount > 0)
        {
            Touch _toque = Input.GetTouch(0);
            Vector2 _posicaoToque = _toque.position;

            if (RectTransformUtility.RectangleContainsScreenPoint(areaAnalogico, _posicaoToque))
            {
                if (_toque.phase == TouchPhase.Moved)
                {
                    posicaoPonto = _toque.position - (Vector2)analogico.position;
                    posicaoPonto = Vector2.ClampMagnitude(posicaoPonto, 50f);

                    pontoAnalogico.position = (Vector2)analogico.position + posicaoPonto;

                    direcaoMovimento = (pontoAnalogico.position - analogico.position).normalized;
                }

                if (_toque.phase == TouchPhase.Ended || _toque.phase == TouchPhase.Canceled)
                {
                    ResetarPosicaoJoystick();
                    direcaoMovimento = Vector3.zero;
                }
            }
            else
            {
                ResetarPosicaoJoystick();
                direcaoMovimento = Vector3.zero;
            }
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