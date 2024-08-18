using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HackManager : MonoBehaviour
{
    [SerializeField] GameObject painelHack;
    [SerializeField] Button botaoFase2;
    [SerializeField] Button botaoFase3;

    int contadorToques = 0;
    float tempoUltimoToque = 0f;
    const float intervaloToques = 0.15f;

    void Start()
    {
        painelHack.SetActive(false);

        botaoFase2.onClick.AddListener(() => CarregarFase(4));
        botaoFase3.onClick.AddListener(() => CarregarFase(6));
    }

    void Update() => VerificarToques();

    void VerificarToques()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Ended && toque.position.x < Screen.width / 2)
            {
                if (Time.time - tempoUltimoToque > intervaloToques)
                {
                    contadorToques = 0;
                }

                contadorToques++;
                tempoUltimoToque = Time.time;

                if (contadorToques >= 6)
                {
                    AbrirPainelHack();
                }
            }
        }
    }

    void AbrirPainelHack()
    {
        painelHack.SetActive(true);
        contadorToques = 0;
    }

    void CarregarFase(int indiceFase) => SceneManager.LoadScene(indiceFase);
}