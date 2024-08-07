using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusTartaruga : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI nickname;

    float vidaAtual;
    void Start()
    {
        //nickname.text = GerenciadorNickname.instance.nickname;
        vidaAtual = 1;
        AtualizarBarra(vidaAtual);
    }

    private void Update()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.forward;
    }

    void AtualizarBarra(float vida)
    {
        healthBar.fillAmount = vida;
    }

    public void ReceberDano(float dano)
    {
        vidaAtual -= dano;
        AtualizarBarra(vidaAtual);
    }

    public void RecuperarVida(float cura)
    {
        vidaAtual += cura;
        AtualizarBarra(vidaAtual);
    }
}
