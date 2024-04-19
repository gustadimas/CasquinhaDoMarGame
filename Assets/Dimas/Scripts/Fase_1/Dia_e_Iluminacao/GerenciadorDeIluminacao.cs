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

    bool atingiu24Horas = false;

    public Action novoDia;
    public static GerenciadorDeIluminacao instance { get; private set; }

    private void Awake() => instance = this;

    private void Update()
    {
        AtualizarIluminacao(HoraDoDia / 24f);

        if (Preset == null) return;

        if (Application.isPlaying)
        {
            HoraDoDia += Time.deltaTime * 24f / (duracaoDiaMinutos * 60f);
            if (HoraDoDia >= 24)
            {
                HoraDoDia = 24;
                if (!atingiu24Horas)
                {
                    Atingiu24Horas();
                    atingiu24Horas = true;
                }
            }
            else
            {
                atingiu24Horas = false;
                AtualizarIluminacao(HoraDoDia / 24f);
            }
        }
    }

    public void ReiniciarDia()
    {
        HoraDoDia = 0;
        atingiu24Horas = false;
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

    void Atingiu24Horas()
    {
        Interface_Passagem.instance.comecarDia?.Invoke();
        Interface_Passagem.instance.StartCoroutine("Aparecer");
    }
}