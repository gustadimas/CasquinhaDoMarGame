using UnityEngine;

public class ClearQuests : MonoBehaviour
{
    [SerializeField] Quest[] missoes;

    private void Awake()
    {
        if (PlayerPrefs.GetFloat("sensibilidade") <= 0f) PlayerPrefs.SetFloat("sensibilidade", 1f);
    }

    public void LimparData()
    {
        for (int i = 0; i < missoes.Length; i++)
        {
            missoes[i].valorAtual = 0;
            missoes[i].estadoMissao = false;
        }
    }
}