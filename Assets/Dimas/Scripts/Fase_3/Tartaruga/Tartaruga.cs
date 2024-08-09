using UnityEngine;

public class Tartaruga : MonoBehaviour
{
    [Header("Configuracoes:")]
    [SerializeField] bool desativarEntradas = false;
    [SerializeField] float velocidadeMovimento;
    [SerializeField] float sensibilidadeRotacao = 2f;
    [SerializeField] float suavidadeRotacao = 5f;
    [SerializeField] float fatorEscalaRotacao = 1f;

    [Header("Atribuicoes:")]
    [SerializeField] RectTransform analogicoMovimento;
    [SerializeField] RectTransform analogicoRotacao;
    public RectTransform areaAnalogicoMovimento;
    public RectTransform areaAnalogicoRotacao;
    [SerializeField] RectTransform pontoAnalogicoMovimento;
    [SerializeField] RectTransform pontoAnalogicoRotacao;
    [SerializeField] Transform cameraTransform;

    Vector3 direcaoMovimento;
    Vector2 posicaoPontoMovimento, posicaoInicialPontoMovimento;
    Vector2 posicaoPontoRotacao, posicaoInicialPontoRotacao;
    Vector2 velocidadeRotacao, velocidadeFrameRotacao;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        posicaoInicialPontoMovimento = pontoAnalogicoMovimento.position;
        posicaoInicialPontoRotacao = pontoAnalogicoRotacao.position;
    }

    private void Update()
    {
        if (!desativarEntradas)
        {
            JoystickMovimento();
            JoystickRotacao();
        }
    }

    private void FixedUpdate()
    {
        if (direcaoMovimento != Vector3.zero)
        {
            Vector3 _movimentoHorizontal = transform.TransformDirection(new Vector3(direcaoMovimento.x, 0, direcaoMovimento.y));
            rb.velocity = _movimentoHorizontal * velocidadeMovimento;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void JoystickMovimento()
    {
        foreach (Touch _toque in Input.touches)
        {
            Vector2 _posicaoToque = _toque.position;

            if (RectTransformUtility.RectangleContainsScreenPoint(areaAnalogicoMovimento, _posicaoToque))
            {
                if (_toque.phase == TouchPhase.Moved || _toque.phase == TouchPhase.Stationary)
                {
                    posicaoPontoMovimento = _toque.position - (Vector2)analogicoMovimento.position;
                    posicaoPontoMovimento = Vector2.ClampMagnitude(posicaoPontoMovimento, 120f);

                    pontoAnalogicoMovimento.position = (Vector2)analogicoMovimento.position + posicaoPontoMovimento;

                    direcaoMovimento = (pontoAnalogicoMovimento.position - analogicoMovimento.position).normalized;
                }

                if (_toque.phase == TouchPhase.Ended || _toque.phase == TouchPhase.Canceled)
                {
                    ResetarPosicaoJoystickMovimento();
                    direcaoMovimento = Vector3.zero;
                }
            }
        }
    }

    void JoystickRotacao()
    {
        foreach (Touch _toque in Input.touches)
        {
            Vector2 _posicaoToque = _toque.position;

            if (RectTransformUtility.RectangleContainsScreenPoint(areaAnalogicoRotacao, _posicaoToque))
            {
                if (_toque.phase == TouchPhase.Moved || _toque.phase == TouchPhase.Stationary)
                {
                    posicaoPontoRotacao = _toque.position - (Vector2)analogicoRotacao.position;
                    posicaoPontoRotacao = Vector2.ClampMagnitude(posicaoPontoRotacao, 120f);

                    pontoAnalogicoRotacao.position = (Vector2)analogicoRotacao.position + posicaoPontoRotacao;

                    Vector2 direcaoRotacao = (pontoAnalogicoRotacao.position - analogicoRotacao.position).normalized;
                    RotacionarTartaruga(direcaoRotacao);
                }

                if (_toque.phase == TouchPhase.Ended || _toque.phase == TouchPhase.Canceled)
                    ResetarPosicaoJoystickRotacao();
            }
        }
    }

    void RotacionarTartaruga(Vector2 direcaoRotacao)
    {
        Vector2 _toqueDelta = direcaoRotacao * fatorEscalaRotacao;
        Vector2 _velBrutaFrame = Vector2.Scale(_toqueDelta, Vector2.one * sensibilidadeRotacao);
        velocidadeFrameRotacao = Vector2.Lerp(velocidadeFrameRotacao, _velBrutaFrame, Time.deltaTime * suavidadeRotacao);
        velocidadeRotacao += velocidadeFrameRotacao * Time.deltaTime;

        velocidadeRotacao.y = Mathf.Clamp(velocidadeRotacao.y, -90, 90);

        transform.localRotation = Quaternion.Euler(-velocidadeRotacao.y, velocidadeRotacao.x, 0);
    }

    void ResetarPosicaoJoystickMovimento()
    {
        posicaoPontoMovimento = Vector2.zero;
        pontoAnalogicoMovimento.position = posicaoInicialPontoMovimento;
    }

    void ResetarPosicaoJoystickRotacao()
    {
        posicaoPontoRotacao = Vector2.zero;
        pontoAnalogicoRotacao.position = posicaoInicialPontoRotacao;
    }

    public void ReativarJogador()
    {
        direcaoMovimento = Vector3.zero;
        ResetarPosicaoJoystickMovimento();
        ResetarPosicaoJoystickRotacao();
    }
}