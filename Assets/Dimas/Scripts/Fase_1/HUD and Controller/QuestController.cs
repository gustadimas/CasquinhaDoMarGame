using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestController : MonoBehaviour
{
    [SerializeField] int proximaFase;

    [Header("Objetos/UI")]
    [SerializeField] TextMeshProUGUI progressoMissoesUI;
    [SerializeField] GameObject espacoMissoes;
    [SerializeField] TextMeshProUGUI[] textoMissoes;
    [SerializeField] Quest[] listaDeMissoes;

    int missoesRestando;
    int missoesCompletas = 0;
    bool faseCompleta = false;

    private void Start()
    {
        missoesRestando = listaDeMissoes.Length;
        progressoMissoesUI.text = "Missões: " + missoesCompletas + "/" + missoesRestando;

        for (int i = 0; i < listaDeMissoes.Length; i++)
        {
            if (listaDeMissoes[i].valorAtual >= listaDeMissoes[i].quantidade)
            {
                listaDeMissoes[i].estadoMissao = true;
            }
            else
            {
                listaDeMissoes[i].estadoMissao = false;
            }

            textoMissoes[i].text = listaDeMissoes[i].valorAtual + "/" + listaDeMissoes[i].quantidade + " " + listaDeMissoes[i].textoMissao;
        }
    }
    public void AtualizarProgressoMissoes(int missaoID, int plus)
    {
        int _i = 0;

        foreach(Quest _missao in listaDeMissoes)
        {
            if(_missao.idMissao == missaoID)
            {
                if (_missao.valorAtual < _missao.quantidade)
                {
                    _missao.valorAtual += plus;
                    textoMissoes[_i].text = listaDeMissoes[_i].valorAtual + "/" + listaDeMissoes[_i].quantidade + " " + listaDeMissoes[_i].textoMissao;
                    
                    if (_missao.valorAtual >= _missao.quantidade)
                    {
                        _missao.estadoMissao = true;
                        textoMissoes[_i].fontStyle = FontStyles.Strikethrough;
                        textoMissoes[_i].color = Color.red;
                        missoesCompletas++;
                        progressoMissoesUI.text = "Missões: " + missoesCompletas + "/" + missoesRestando;
					}
                }
            }
            _i++;
        }

        if(missoesCompletas >= listaDeMissoes.Length && !faseCompleta)
        {
            faseCompleta = true;
            EstagioCompleto();
        }
    }

    public bool ChecarEstadoMissao(int missaoID) 
    {
		bool estado = false;

		foreach (Quest _missao in listaDeMissoes) 
        {
            if(_missao.idMissao == missaoID)
            {
                estado = _missao.estadoMissao;
			}
        }
		return estado;
    }

    public void EstagioCompleto()
    {
        GameManager.levelsComplete += 1;

		if (GameManager.levelsComplete >= 7) 
        {
            proximaFase = 10;
		}

		GameManager.instance.LoadScene(proximaFase);
	}
}
