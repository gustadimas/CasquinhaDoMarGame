using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Accessibility : MonoBehaviour
{
    [SerializeField] List<Outline> contornos;
    [SerializeField] Toggle alternarContorno;

    [Header("Opcoes")]
    [SerializeField] Image exemploContornoImg;
    [SerializeField] Sprite contornoImgPadrao;
    [SerializeField] Sprite contornoImgSelecionado;

	int contornoAtivo;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            contornoAtivo = PlayerPrefs.GetInt("Contorno");
            if (contornoAtivo == 0)
            {
                ContornoEstado(false);
                alternarContorno.isOn = false;
            }
            else
            {
                ContornoEstado(true);
                alternarContorno.isOn = true;
            }
        }
    }
    
    public void ContornoEstado(bool estado)
    {
        for(int i=0; i< contornos.Count; i++)
        {
            contornos[i].enabled = estado;
        }

        if(estado)
        {
            PlayerPrefs.SetInt("Contorno", 1);

            exemploContornoImg.sprite = contornoImgSelecionado;
        }
        else
        {
            PlayerPrefs.SetInt("Contorno", 0);
            exemploContornoImg.sprite = contornoImgPadrao;
        }
    }

	public void ContornoEstadoIndividual(int contorno, bool estado)
	{
		for (int i = 0; i < contornos.Count; i++)
		{
            contornos[contorno].enabled = estado;
		}
	}
}
