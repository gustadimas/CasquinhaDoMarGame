using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AdicionarXP()
    {
        xpAtual += 0.35f;
        VerificarLevelUp();
    }

    void VerificarLevelUp()
    {
        if (xpAtual >= xpNecessario)
        {
            Interface_Passagem.instance.StartCoroutine(nameof(Interface_Passagem.Aparecer));
            Interface_Passagem.instance.comecarDia?.Invoke();
            fotinha.sprite = icones[1];
            Invoke(nameof(AtualizarDia), 2f);
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
}
