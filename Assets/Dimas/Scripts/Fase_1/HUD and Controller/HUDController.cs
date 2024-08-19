using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public bool abrirMissoes = false;
    public static HUDController instance;
    public GameObject missaoUI_img;

    private void Start() => instance = this;

    public void JogarBT() => SceneManager.LoadScene(1);

    public void SairBT()
    {
        PlayerPrefs.DeleteKey("NicknameFilhote");
        Application.Quit();
    }

    public void VotarMenuBT()
    {
        SceneManager.LoadScene(0);
        GameManager.proximaEtapa = 0;
        PlayerPrefs.DeleteKey("NicknameFilhote");
    }

    public void MissaoUIAtiva()
    {
        abrirMissoes = !abrirMissoes;
        missaoUI_img.SetActive(abrirMissoes);
    }
}