using UnityEngine;
using System;

[ExecuteAlways]
public class GerenciadorDeIluminacao : MonoBehaviour
{
    [Header("Luz e preset de Luz:")]
    [SerializeField] Light luzDirecional;
    [SerializeField] PresetDeIluminacao preset;

    [Header("Definir a Hora Manual:")]
    [SerializeField, Range(6, 24)] float horaDoDia;

    [Header("Definir a Hora Automatica:")]
    [SerializeField] float duracaoDiaMinutos;

    [SerializeField] float totalXP;

    public static bool atingiu24Horas = false;
    public static bool atualizarDia = false;

    public Action novoDia;
    public static GerenciadorDeIluminacao instance { get; private set; }

    private void Awake() => instance = this;


    private void Update() => ComecarDia();

    void ComecarDia()
    {
        if (atualizarDia == true)
        {
            AtualizarIluminacao(horaDoDia / 24f);

            if (preset == null) return;

            if (Application.isPlaying)
            {
                horaDoDia += Time.deltaTime * 24f / (duracaoDiaMinutos * 60f);
                if (horaDoDia >= 24)
                {
                    horaDoDia = 24;

                    if (!atingiu24Horas) 
                        atingiu24Horas = true;
                }
                else
                {
                    atingiu24Horas = false;
                    AtualizarIluminacao(horaDoDia / 24f);
                }
            }
        }
    }

    public void ReiniciarDia()
    {
        HUDController.instance.missaoUI_img.gameObject.SetActive(false);
        horaDoDia = 6;
        atingiu24Horas = false;
        AtualizarIluminacao(horaDoDia / 24f);
        foreach (Quest _missao in QuestController.instance.missoesDoDiaAtual.quests) _missao.Resetar();
        QuestController.instance.diaAtual++;
        QuestController.instance.MissoesDiaAtual_Set();
    }

    void AtualizarIluminacao(float _percentualDeTempo)
    {
        if (preset == null)
        {
            Debug.LogError("Preset de iluminação é nulo!");
            return;
        }

        if (luzDirecional == null)
        {
            Debug.LogError("Luz direcional é nula!");
            return;
        }

        RenderSettings.ambientLight = preset.corAmbiente.Evaluate(_percentualDeTempo);
        RenderSettings.fogColor = preset.corNeblina.Evaluate(_percentualDeTempo);

        luzDirecional.color = preset.corDirecional.Evaluate(_percentualDeTempo);
        luzDirecional.transform.localRotation = Quaternion.Euler(new Vector3((_percentualDeTempo * 360f) - 90f, 170f, 0));
    }

    private void OnValidate()
    {
        if (luzDirecional == null)
        {
            if (RenderSettings.sun != null)
            {
                luzDirecional = RenderSettings.sun;
            }
            else
            {
                Light[] _lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light _light in _lights)
                {
                    if (_light.type == LightType.Directional)
                    {
                        luzDirecional = _light;
                        return;
                    }
                }
            }
        }
    }
}