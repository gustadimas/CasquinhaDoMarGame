using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaTartarugas : MonoBehaviour
{
    [SerializeField] GameObject[] tartarugas, iconesTartaruga;
    int contagemTartarugas;
    public static VidaTartarugas instance;

    private void Start()
    {
        instance = this;
        contagemTartarugas = tartarugas.Length-1;
    }

    public void PerdeuVida()
    {
        tartarugas[contagemTartarugas].SetActive(false);
        iconesTartaruga[contagemTartarugas].SetActive(false);
        contagemTartarugas--;

        if (contagemTartarugas < 0)
            GameManager.instance.LoadScene(GameManager.proximaEtapa);
    }
}