using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaTartarugas : MonoBehaviour
{
    [SerializeField] GameObject[] tartarugas;
    int contagemTartarugas;

    private void Start()
    {
        contagemTartarugas = tartarugas.Length-1;
        tartarugas = GameObject.FindGameObjectsWithTag("Filhote");
    }

    public void PerdeuVida()
    {
        Destroy(tartarugas[contagemTartarugas]);
        contagemTartarugas--;

        if (contagemTartarugas < 0)
        {
            SceneManager.LoadScene("LipeFase2");
        }
    }
}
