using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Missao", menuName = "Missoes/Nova Missao")]
public class Quest : ScriptableObject
{
    public int idMissao;
    public int valorAtual;
    public int quantidade;
    public bool estadoMissao;
    public string textoMissao;

    public void Resetar()
    {
        valorAtual = 0;
        estadoMissao = false;
    }
}