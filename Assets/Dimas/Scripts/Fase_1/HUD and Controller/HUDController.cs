using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public bool abrirMissoes = false;
    public static HUDController instance;
    public GameObject missaoUI_img;

    private void Start() => instance = this;

    public void JogarBT() => SceneManager.LoadScene(1);

    public void SairBT() => Application.Quit();

    public void MissaoUIAtiva()
    {
        abrirMissoes = !abrirMissoes;
        missaoUI_img.SetActive(abrirMissoes);
    }
}