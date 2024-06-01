using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GanhoXP : MonoBehaviour
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
    }

    //QUANDO CONCLUIR A QUEST, CHAMAR O MÉTODO ADICIONARXP
    public void AdicionarXP()
    {
        xpAtual += 0.35f;
        VerificarLevelUp();
    }
    //-----------------------------------//

    void VerificarLevelUp()
    {
        if (xpAtual >= xpNecessario)
        {
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
        if (numDia < maxDias)
        {
            numDia++;
        }
    }
}
