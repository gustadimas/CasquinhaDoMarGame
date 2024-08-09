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

    GameObject audioObj;
    AudioSource varrerSom;
    private void Start()
    {
        cam = Camera.main;
        audioObj = GameObject.Find("AudioVarrer");
        varrerSom = audioObj.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch _toque = Input.GetTouch(0);
            Vector3 _posicaoToque = cam.ScreenToWorldPoint(new Vector3(_toque.position.x, _toque.position.y, cam.nearClipPlane));

            Ray _ray = cam.ScreenPointToRay(_toque.position);
            RaycastHit hit;

            if (_toque.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(_ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        estaSendoArrastado = true;
                        posicaoToqueAnterior = _toque.position;
                    }
                }
            }

            if (_toque.phase == TouchPhase.Moved && estaSendoArrastado)
            {
                distanciaMovimento += Vector2.Distance(_toque.position, posicaoToqueAnterior);
                posicaoToqueAnterior = _toque.position;

                if (distanciaMovimento >= distanciaNecessaria)
                {
                    contadorToques++;
                    distanciaMovimento = 0f;
                }
            }

            if (_toque.phase == TouchPhase.Ended || _toque.phase == TouchPhase.Canceled)
                estaSendoArrastado = false;

            if (contadorToques >= toquesNecessarios)
                LimparLixo();
        }
    }

    private void LimparLixo()
    {
        varrerSom.Play();
        Debug.Log("Lixo limpo após " + contadorToques + " toques.");
        Destroy(gameObject);
    }
}