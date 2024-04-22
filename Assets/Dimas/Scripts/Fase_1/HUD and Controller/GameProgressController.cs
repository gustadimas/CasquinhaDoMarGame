using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameProgressController : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] Color[] espacoCor = new Color[2];

    [SerializeField] AchievementSlotUI[] espacoConquistaUI;

    [Header("Scriptable Objects")]
    [SerializeField] Achievement[] conquista;


    private void Awake()
    {
        for (int i = 0; i < espacoConquistaUI.Length; i++)
        {
            espacoConquistaUI[i].titulo.text = conquista[i].titulo;
            espacoConquistaUI[i].descricao.text = conquista[i].descricao;
            //espacoConquistaUI[i].image.sprite = conquista[i].image;
            //if (conquista[i].isCompleted)
            //{
            //    espacoConquistaUI[i].slotAchiement.color = colorSlot[1];
            //}
            //else
            //{
            //    espacoConquistaUI[i].slotAchiement.color = colorSlot[0];
            //}
        }
    }
}
