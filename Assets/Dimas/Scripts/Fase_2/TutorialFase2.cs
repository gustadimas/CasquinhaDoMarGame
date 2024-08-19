using UnityEngine;
using System.Collections;

public class TutorialFase2 : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] GameObject objetoComAnimacao;
    [SerializeField] float tempoEsperaAntesLento = 2.5f;
    [SerializeField] float cameraLentaTimeScale = 0.1f;

    bool tutorialAtivo = false;

    private void Start() => objetoComAnimacao.SetActive(false);

    public void QuadricicloSpawnado()
    {
        StartCoroutine(IniciarCameraLenta());
    }

    IEnumerator IniciarCameraLenta()
    {
        yield return new WaitForSeconds(tempoEsperaAntesLento);

        objetoComAnimacao.SetActive(true);
        Time.timeScale = cameraLentaTimeScale;
        tutorialAtivo = true;
    }

    private void Update()
    {
        if (tutorialAtivo && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            EncerrarTutorial();
    }

    void EncerrarTutorial()
    {
        objetoComAnimacao.SetActive(false);
        Time.timeScale = 1f;
        tutorialAtivo = false;
    }
}