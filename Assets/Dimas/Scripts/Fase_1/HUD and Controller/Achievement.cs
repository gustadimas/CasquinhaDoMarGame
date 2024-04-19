using UnityEngine;

[CreateAssetMenu(fileName = "Conquista", menuName = "Conquistas/Nova_Conquista")]
public class Achievement : ScriptableObject
{
    [Header("Info")]
    public int id;
    public string titulo;
    [TextArea]
    public string descricao;
    [Header("Parametros")]
    public int valorAtual;
    public int quantidadeParaCompletar;
    public bool completou;
}
