using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Reflection;

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
    private void Start()
    {
        xpAtual = 0;
        numDia = 1;
        fotinha.sprite = icones[0];
        AtualizarInterface();
    }

    private void Update()
    {
        AtualizarInterface();

        if (GerenciadorDeIluminacao.atingiu24Horas == true && xpAtual < xpNecessario)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [ContextMenu("Adiantar")]
    public void AdicionarXP()
    {
        xpAtual += 0.35f;
        VerificarLevelUp();
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
        txtDia.text = "Dia " + numDia + "/5";
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
            ControleXP _componente = (ControleXP) target;
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
