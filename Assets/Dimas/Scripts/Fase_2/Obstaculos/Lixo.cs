using UnityEngine;

public class Lixo : MonoBehaviour
{
    Camera cam;
    int contadorToques = 0;
    const int toquesNecessarios = 50;
    float distanciaMovimento = 0f;
    const float distanciaNecessaria = 25f;

    Vector2 posicaoToqueAnterior;
    bool estaSendoArrastado = false;

    private void Start() => cam = Camera.main;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            Vector3 posicaoToque = cam.ScreenToWorldPoint(new Vector3(toque.position.x, toque.position.y, cam.nearClipPlane));

            Ray ray = cam.ScreenPointToRay(toque.position);
            RaycastHit hit;

            if (toque.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        estaSendoArrastado = true;
                        posicaoToqueAnterior = toque.position;
                    }
                }
            }

            if (toque.phase == TouchPhase.Moved && estaSendoArrastado)
            {
                distanciaMovimento += Vector2.Distance(toque.position, posicaoToqueAnterior);
                posicaoToqueAnterior = toque.position;

                if (distanciaMovimento >= distanciaNecessaria)
                {
                    contadorToques++;
                    distanciaMovimento = 0f;
                }
            }

            if (toque.phase == TouchPhase.Ended || toque.phase == TouchPhase.Canceled)
                estaSendoArrastado = false;

            if (contadorToques >= toquesNecessarios)
                LimparLixo();
        }
    }

    private void LimparLixo()
    {
        Debug.Log("Lixo limpo após " + contadorToques + " toques.");
        Destroy(gameObject);
    }
}
