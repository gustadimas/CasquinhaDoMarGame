using UnityEngine;

public class HUDController : MonoBehaviour
{
    bool abrirMissoes = false;

    //[Header("Link Objects UI")]
    //[SerializeField] private Slider efeitosSonorosSlider;
    //[SerializeField] private Slider musicaSlider;
    //[SerializeField] private Slider sensibilidadeSlider;
    [SerializeField] private GameObject missaoUI_img;

    //[Header("Mixers")]
    //[SerializeField] private AudioMixer mixerMusica;
    //[SerializeField] private AudioMixer mixerEfeitos;

    //[Header("Menu")]
    //[SerializeField] private Image contornoBT;
    //[SerializeField] private TextMeshProUGUI textoContornoBT;
    //[SerializeField] private Sprite contornoPadrao;
    //[SerializeField] private Sprite contornoSelecionado;

    //private int contornoAtivo = 0;


    private void Start()
    {
        //if (SceneManager.GetActiveScene().buildIndex == 0)
        //{
        //    contornoAtivo = PlayerPrefs.GetInt("Outline");
        //    if (contornoAtivo == 0) contornoAtivo = 1;
        //    else contornoAtivo = 0;
        //    ContornoBT();
        //}

        //efeitosSonorosSlider.value = PlayerPrefs.GetFloat("volumeEfeitos");
        //musicaSlider.value = PlayerPrefs.GetFloat("volumeMusica");
        //sensibilidadeSlider.value = PlayerPrefs.GetFloat("sensibilidade");
    }

    public void MissaoUIAtiva()
    {
        abrirMissoes = !abrirMissoes;
        missaoUI_img.SetActive(abrirMissoes);
    }

    //   public void JogarBT()
    //   {
    //       GameManager.levelsComplete = 0;
    //       //GameManager.instance.LoadScene(9);
    //   }

    //   public void SairBT()
    //   {
    //       Application.Quit();
    //   }

    //   public void SetarVolumeMusica(float volumeMusica)
    //   {
    //       mixerMusica.SetFloat("volumeMusica", volumeMusica);
    //       PlayerPrefs.SetFloat("volumeMusica", volumeMusica);
    //   }

    //   public void SetarVolumeEfeitos(float volumeEfeitos)
    //   {
    //       mixerEfeitos.SetFloat("effectVolume", volumeEfeitos);
    //       PlayerPrefs.SetFloat("effectVolume", volumeEfeitos);
    //}

    //   public void AparecerOpcoes(CanvasGroup canvasGroup, RectTransform rectTransform, float fadeTime) 
    //   {
    //       canvasGroup.alpha = 0f;
    //       rectTransform.transform.localPosition = new Vector3(-1000f, rectTransform.localPosition.y, 0f);
    //       rectTransform.DOAnchorPos(new Vector2(0, 0f), fadeTime, false).SetEase(Ease.OutQuint);
    //       canvasGroup.DOFade(1, fadeTime);
    //   }

    //   public void DesaparecerOpcoes(CanvasGroup canvasGroup, RectTransform rectTransform, float fadeTime) 
    //   {
    //	canvasGroup.alpha = 1f;
    //	rectTransform.DOAnchorPos(new Vector2(-1000f, 0f), fadeTime, false).SetEase(Ease.InOutQuint);
    //	canvasGroup.DOFade(0, fadeTime);
    //}

    //public void ContornoBT()
    //{
    //	if (contornoAtivo == 0)
    //	{
    //           contornoAtivo = 1;
    //		contornoBT.sprite = contornoSelecionado;
    //           textoContornoBT.text = "DESATIVAR  \n BORDA \n (OBJETOS)";
    //	}
    //	else
    //	{
    //           contornoAtivo = 0;
    //           contornoBT.sprite = contornoPadrao;
    //           textoContornoBT.text = "ATIVAR \n BORDA \n (OBJETOS)";
    //	}

    //	PlayerPrefs.SetInt("Contorno", contornoAtivo);
    //}

    //public void SensibilidadeSlider()
    //   {
    //       PlayerPrefs.SetFloat("sensibilidade", sensibilidadeSlider.value);
    //   }

    //   public void CarregarCena(int valor)
    //   {
    //       SceneManager.LoadScene(valor);
    //   }
}