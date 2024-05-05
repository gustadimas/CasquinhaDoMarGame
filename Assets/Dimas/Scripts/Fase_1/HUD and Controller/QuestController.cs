using UnityEngine;
using TMPro;

public class QuestController : MonoBehaviour
{
    public static QuestController instance;
    [SerializeField] int proximaFase;

    [Header("Objetos/UI")]
    [SerializeField] TextMeshProUGUI progressoMissoesUI;
    [SerializeField] GameObject espacoMissoes;
    [SerializeField] TextMeshProUGUI[] textoMissoes;
    [SerializeField] MissoesDia[] missoesDeCadaDia;
    public MissoesDia missoesDoDiaAtual;

    [HideInInspector] public int diaAtual = 1;

    [SerializeField] ControleXP controleXP;

    int missoesRestando;
    int missoesCompletas = 0;
    //bool faseCompleta = false;

    private void Start()
    {
        instance = this;
        
        MissoesDiaAtual_Set();
    }

    public void MissoesDiaAtual_Set()
    {
        missoesDoDiaAtual.quests = missoesDeCadaDia[diaAtual - 1].quests;
        TextoDasMissoes_Set();
    }

    void TextoDasMissoes_Set()
    {
        foreach (Quest _missao in missoesDoDiaAtual.quests) _missao.Resetar();

        missoesCompletas = 0;
        missoesRestando = missoesDoDiaAtual.quests.Length;

        progressoMissoesUI.text = "Missões: " + missoesCompletas + "/" + missoesRestando;

        for (int i = 0; i < missoesDoDiaAtual.quests.Length; i++)
        {
            if (missoesDoDiaAtual.quests[i].valorAtual >= missoesDoDiaAtual.quests[i].quantidade)
            {
                missoesDoDiaAtual.quests[i].estadoMissao = true;
            }
            else
            {
                missoesDoDiaAtual.quests[i].estadoMissao = false;
            }

            textoMissoes[i].text = missoesDoDiaAtual.quests[i].valorAtual + "/" + missoesDoDiaAtual.quests[i].quantidade + " " + missoesDoDiaAtual.quests[i].textoMissao;

            textoMissoes[i].fontStyle = FontStyles.Normal;
            textoMissoes[i].color = Color.black;
        }
    }

    public void AtualizarProgressoMissoes(int missaoID, int plus)
    {
        int _i = 0;

        foreach (Quest _missao in missoesDoDiaAtual.quests)
        {
            if (_missao.idMissao == missaoID)
            {
                if (_missao.valorAtual < _missao.quantidade)
                {
                    _missao.valorAtual += plus;
                    textoMissoes[_i].text = missoesDoDiaAtual.quests[_i].valorAtual + "/" + missoesDoDiaAtual.quests[_i].quantidade + " " + missoesDoDiaAtual.quests[_i].textoMissao;

                    if (_missao.valorAtual >= _missao.quantidade)
                    {
                        EventController.eventoEmAndamento = false;
                        _missao.estadoMissao = true;
                        textoMissoes[_i].fontStyle = FontStyles.Strikethrough;
                        textoMissoes[_i].color = Color.green;
                        missoesCompletas++;
                        progressoMissoesUI.text = "Missões: " + missoesCompletas + "/" + missoesRestando;

                        controleXP.AdicionarXP();
                    }
                }
            }
            _i++;
        }

        //if(missoesCompletas >= missoesDoDiaAtual.quests.Length && !faseCompleta)
        //{
        //    faseCompleta = true;
        //    EstagioCompleto();
        //}
    }

    public bool ChecarEstadoMissao(int missaoID)
    {
        bool _estado = false;

        foreach (Quest _missao in missoesDoDiaAtual.quests)
        {
            if (_missao.idMissao == missaoID)
                _estado = _missao.estadoMissao;
        }
        return _estado;
    }

    //   public void EstagioCompleto()
    //   {
    //       GameManager.levelsComplete += 1;

    //	if (GameManager.levelsComplete >= 7) 
    //       {
    //           proximaFase = 10;
    //	}

    //	GameManager.instance.LoadScene(proximaFase);
    //}
}
