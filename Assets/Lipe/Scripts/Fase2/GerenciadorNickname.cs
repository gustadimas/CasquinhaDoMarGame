using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GerenciadorNickname : MonoBehaviour
{
    [SerializeField] TMP_InputField inputName;
    [SerializeField] TMP_Text txtNickname;
    [SerializeField] GameObject panelNickname;
    public string nickname;
    public static GerenciadorNickname instance;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 0;
    }
    public void Nomear()
    {
        if (inputName != null)
        {
            nickname = inputName.text;
            txtNickname.text = nickname;
            panelNickname.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
