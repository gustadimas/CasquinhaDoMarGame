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

        if (GerenciadorDeIluminacao.atingiu14Horas == true && xpAtual < xpNecessario)
            GameManager.instance.LoadScene(0);
    }

    [ContextMenu("Adiantar")]
    public void AdicionarXP()
    {
        StartCoroutine(AjustarOpacidade(barraXP, 1f, 0.5f));
        StartCoroutine(AdicionarXPGradualmente(0.35f));
        StartCoroutine(FazerBarraDesaparecer());
    }

    IEnumerator AdicionarXPGradualmente(float valorXP)
    {
        float tempoInicio = Time.time;
        float tempoDuracao = 1f;
        float xpInicial = xpAtual;

        while (Time.time - tempoInicio < tempoDuracao)
        {
            xpAtual = Mathf.Lerp(xpInicial, xpInicial + valorXP, (Time.time - tempoInicio) / tempoDuracao);
            yield return null;
        }

        xpAtual = xpInicial + valorXP;
        VerificarLevelUp();
    }

    IEnumerator AjustarOpacidade(CanvasGroup grupo, float valorFinal, float duracao)
    {
        float tempoInicio = Time.time;
        float opacidadeInicial = grupo.alpha;

        while (Time.time - tempoInicio < duracao)
        {
            float novaOpacidade = Mathf.Lerp(opacidadeInicial, valorFinal, (Time.time - tempoInicio) / duracao);
            grupo.alpha = novaOpacidade;
            yield return null;
        }

        grupo.alpha = valorFinal;
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
            Interface_Passagem.Instance.StartCoroutine(nameof(Interface_Passagem.Aparecer));

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
