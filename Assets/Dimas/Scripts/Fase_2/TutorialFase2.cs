using UnityEngine;
using System.Collections;

public class TutorialFase2 : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] GameObject objetoComAnimacaoQuadriciclo;
    [SerializeField] GameObject objetoComAnimacaoLixo;
    [SerializeField] float tempoEsperaAntesLento = 2.5f;
    [SerializeField] float cameraLentaTimeScale = 0.1f;
    [SerializeField] float duracaoMaximaTutorial = 10f;

    bool tutorialQuadricicloAtivo = false;
    public bool tutorialLixoAtivo = false;

    private void Start()
    {
        objetoComAnimacaoQuadriciclo.SetActive(false);
        objetoComAnimacaoLixo.SetActive(false);
    }

    public void QuadricicloSpawnado() => StartCoroutine(IniciarCameraLentaQuadriciclo());

    public void LixoTutorial() => StartCoroutine(IniciarCameraLentaLixo());

    IEnumerator IniciarCameraLentaQuadriciclo()
    {
        yield return new WaitForSeconds(tempoEsperaAntesLento);

        objetoComAnimacaoQuadriciclo.SetActive(true);
        Time.timeScale = cameraLentaTimeScale;
        tutorialQuadricicloAtivo = true;

        yield return new WaitForSecondsRealtime(duracaoMaximaTutorial);

        if (tutorialQuadricicloAtivo)
            EncerrarTutorialQuadriciclo();
    }

    IEnumerator IniciarCameraLentaLixo()
    {
        yield return new WaitForSeconds(tempoEsperaAntesLento);

        objetoComAnimacaoLixo.SetActive(true);
        Time.timeScale = cameraLentaTimeScale;
        tutorialLixoAtivo = true;

        yield return new WaitForSecondsRealtime(duracaoMaximaTutorial);

        if (tutorialLixoAtivo)
            EncerrarTutorialLixo();
    }

    private void Update()
    {
        if (tutorialQuadricicloAtivo && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            EncerrarTutorialQuadriciclo();

        if (tutorialLixoAtivo && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            EncerrarTutorialLixo();
    }

    void EncerrarTutorialQuadriciclo()
    {
        objetoComAnimacaoQuadriciclo.SetActive(false);
        Time.timeScale = 1f;
        tutorialQuadricicloAtivo = false;
    }

    void EncerrarTutorialLixo()
    {
        objetoComAnimacaoLixo.SetActive(false);
        Time.timeScale = 1f;
        tutorialLixoAtivo = false;
    }
}