using UnityEngine;
using System;

[ExecuteAlways]
public class GerenciadorDeIluminacao : MonoBehaviour
{
    [Header("Luz e Preset de Luz:")]
    [SerializeField] Light LuzDirecional;
    [SerializeField] PresetDeIluminacao Preset;

    [Header("Definir a Hora Manual:")]
    [SerializeField, Range(7, 14)] float HoraDoDia;

    [Header("Definir a Hora Automatica:")]
    [SerializeField] float duracaoDiaMinutos;

    [Header("Intensidade da Luz Direcional:")]
    [SerializeField] float intensidadeLuzDirecional = 1f;

    [SerializeField] float totalXP;

    public static bool atingiu14Horas = false;
    public static bool atualizarDia = false;

    public Action novoDia;
    public static GerenciadorDeIluminacao instance { get; private set; }

    private void Awake() => instance = this;

    private void Update() => ComeçarDia();

    void ComeçarDia()
    {
        if (atualizarDia == true)
        {
            AtualizarIluminacao((HoraDoDia - 7f) / 7f);
            if (Preset == null) return;

            if (Application.isPlaying)
            {
                HoraDoDia += Time.deltaTime * 7f / (duracaoDiaMinutos * 60f);
                if (HoraDoDia >= 14)
                {
                    HoraDoDia = 14;
                    if (!atingiu14Horas) atingiu14Horas = true;
                }
                else
                {
                    atingiu14Horas = false;
                    AtualizarIluminacao((HoraDoDia - 7f) / 7f);
                }
            }
        }
    }

    public void ReiniciarDia()
    {
        HUDController.instance.missaoUI_img.gameObject.SetActive(false);
        HoraDoDia = 7;
        atingiu14Horas = false;
        AtualizarIluminacao((HoraDoDia - 7f) / 7f);
        foreach (Quest _missao in QuestController.instance.missoesDoDiaAtual.quests) _missao.Resetar();
        QuestController.instance.diaAtual++;
        QuestController.instance.MissoesDiaAtual_Set();
    }

    void AtualizarIluminacao(float _percentualDeTempo)
    {
        if (LuzDirecional != null)
        {
            LuzDirecional.transform.localRotation = Quaternion.Euler(new Vector3((_percentualDeTempo * 360f) - 90f, 170f, 0));
            LuzDirecional.intensity = intensidadeLuzDirecional;
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
