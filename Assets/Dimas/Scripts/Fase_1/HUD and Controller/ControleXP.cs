using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Reflection;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ControleXP : MonoBehaviour
{
    [SerializeField] int xpNecessario, maxDias;
    int nivelAtual, numDia;
    float xpAtual;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI txtDia;
    [SerializeField] Image preenchimentoXP, fotinha;
    [SerializeField] Sprite[] icones;
    CanvasGroup barraXP;

    private void Start()
    {
        xpAtual = 0;
        numDia = 1;
        fotinha.sprite = icones[0];
        barraXP = GetComponent<CanvasGroup>();
        AtualizarInterface();
    }

    private void Update()
    {
        AtualizarInterface();

        if (GerenciadorDeIluminacao.atingiu24Horas == true && xpAtual < xpNecessario)
        {
            ReiniciarCena();
        }
    }

    [ContextMenu("Adiantar")]
    public void AdicionarXP()
    {
        StartCoroutine(AjustarOpacidade(barraXP, 1f, 0.5f));
        StartCoroutine(AdicionarXPGradualmente(0.35f));
        StartCoroutine(FazerBarraDesaparecer());
    }

    IEnumerator AdicionarXPGradualmente(float _valorXP)
    {
        float _tempoInicio = Time.time;
        float _tempoDuracao = 1f;
        float _xpInicial = xpAtual;

        while (Time.time - _tempoInicio < _tempoDuracao)
        {
            xpAtual = Mathf.Lerp(_xpInicial, _xpInicial + _valorXP, (Time.time - _tempoInicio) / _tempoDuracao);
            yield return null;
        }

        xpAtual = _xpInicial + _valorXP;
        VerificarLevelUp();
    }

    IEnumerator AjustarOpacidade(CanvasGroup _grupo, float _valorFinal, float _duracao)
    {
        float _tempoInicio = Time.time;
        float _opacidadeInicial = _grupo.alpha;

        while (Time.time - _tempoInicio < _duracao)
        {
            float _novaOpacidade = Mathf.Lerp(_opacidadeInicial, _valorFinal, (Time.time - _tempoInicio) / _duracao);
            _grupo.alpha = _novaOpacidade;
            yield return null;
        }

        _grupo.alpha = _valorFinal;
    }

    IEnumerator FazerBarraDesaparecer()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(AjustarOpacidade(barraXP, 0f, 0.5f));
    }

    void VerificarLevelUp()
    {
        if (xpAtual >= xpNecessario)
        {
            Interface_Passagem.instance.StartCoroutine(nameof(Interface_Passagem.Aparecer));
            fotinha.sprite = icones[1];
            Invoke(nameof(AtualizarDia), 5f);
        }
    }

    void AtualizarInterface()
    {
        preenchimentoXP.fillAmount = xpAtual;
        txtDia.text = "Dia " + numDia + "/3";
    }

    void AtualizarDia()
    {
        xpAtual = 0;
        fotinha.sprite = icones[0];
        if (numDia < maxDias) numDia++;
    }

    private void ReiniciarCena()
    {
        Debug.Log("Reiniciando Cena");
        GerenciadorDeIluminacao.atingiu24Horas = false;
        FadeManager.instance.CarregarCenaAtualComFade();
        GameManager.diasCompletos = 0;
        GerenciadorDeIluminacao.atualizarDia = false;
        Interface_Passagem.instance.StartCoroutine(nameof(Interface_Passagem.Desaparecer));
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ControleXP))]
    public class ContextMenuButton : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ControleXP _componente = (ControleXP)target;
            MethodInfo[] _metodos = typeof(ControleXP).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (MethodInfo _metodo in _metodos)
            {
                var _atributos = _metodo.GetCustomAttributes(typeof(ContextMenu), false);
                if (_atributos.Length > 0)
                {
                    var _contextMenu = _atributos[0] as ContextMenu;
                    if (GUILayout.Button(_contextMenu.menuItem))
                        _metodo.Invoke(_componente, null);
                }
            }
        }
    }
#endif
}