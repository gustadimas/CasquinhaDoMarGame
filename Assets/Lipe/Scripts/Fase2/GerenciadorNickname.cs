using UnityEngine;
using TMPro;

public class GerenciadorNickname : MonoBehaviour
{
    [SerializeField] TMP_InputField inputName;
    [SerializeField] TMP_Text txtNickname;
    [SerializeField] GameObject panelNickname;
    public string nickname;
    public static GerenciadorNickname instance;
    public bool comecou;

    private void Awake()
    {
        instance = this;
        PlayerPrefs.DeleteKey("NicknameFilhote");

        FadeManager.instance.ConfigurarUnscaledTime(true);
        Time.timeScale = 0;
        comecou = false;
    }

    public void Nomear()
    {
        if (inputName != null && !string.IsNullOrEmpty(inputName.text.Trim()))
        {
            nickname = inputName.text;
            txtNickname.text = nickname;
            panelNickname.SetActive(false);
            FadeManager.instance.ConfigurarUnscaledTime(false);
            Time.timeScale = 1f;
            comecou = true;

            PlayerPrefs.SetString("NicknameFilhote", nickname);
            PlayerPrefs.Save();
        }
    }
}
