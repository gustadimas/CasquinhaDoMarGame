using UnityEngine;
using System;

[ExecuteAlways]
public class GerenciadorDeIluminacao : MonoBehaviour
{
    [Header("Luz e Preset de Luz:")]
    [SerializeField] Light LuzDirecional;
    [SerializeField] PresetDeIluminacao Preset;

    [Header("Definir a Hora Manual:")]
    [SerializeField, Range(5, 24)] float HoraDoDia;

    [Header("Definir a Hora Automatica:")]
    [SerializeField] float duracaoDiaMinutos;

    [SerializeField] float totalXP;

    public static bool atingiu24Horas = false;
    public static bool atualizarDia = false;

    public Action novoDia;
    public static GerenciadorDeIluminacao instance { get; private set; }

    private void Awake() => instance = this;

    private void Update() => ComeçarDia();

    void ComeçarDia()
    {
        if (atualizarDia == true)
        {
            AtualizarIluminacao(HoraDoDia / 24f);

            if (Preset == null) return;

            if (Application.isPlaying)
            {
                print("awdawdawd");
                HoraDoDia += Time.deltaTime * 24f / (duracaoDiaMinutos * 60f);
                if (HoraDoDia >= 24)
                {
                    HoraDoDia = 24;
                    if (!atingiu24Horas) atingiu24Horas = true;
                }
                else
                {
                    atingiu24Horas = false;
                    AtualizarIluminacao(HoraDoDia / 24f);
                }
            }
        }
    }

    public void ReiniciarDia()
    {
        HoraDoDia = 5;
        atingiu24Horas = false;
        AtualizarIluminacao(HoraDoDia / 24f);
    }

    void AtualizarIluminacao(float _percentualDeTempo)
    {
        RenderSettings.ambientLight = Preset.CorAmbiente.Evaluate(_percentualDeTempo);
        RenderSettings.fogColor = Preset.CorNeblina.Evaluate(_percentualDeTempo);

        if (LuzDirecional != null)
        {
            LuzDirecional.color = Preset.CorDirecional.Evaluate(_percentualDeTempo);
            LuzDirecional.transform.localRotation = Quaternion.Euler(new Vector3((_percentualDeTempo * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (LuzDirecional != null) return;

        if (RenderSettings.sun != null) LuzDirecional = RenderSettings.sun;

        else
        {
            Light[] _lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light _light in _lights)
            {
                if (_light.type == LightType.Directional)
                {
                    LuzDirecional = _light;
                    return;
                }
            }
        }
    }
}