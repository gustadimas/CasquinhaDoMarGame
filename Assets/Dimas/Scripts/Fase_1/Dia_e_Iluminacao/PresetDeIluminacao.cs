using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Preset_de_Iluminacao", menuName = "Scriptables/Preset_de_Iluminacao", order = 1)]
public class PresetDeIluminacao : ScriptableObject
{
    public Gradient corAmbiente;
    public Gradient corDirecional;
    public Gradient corNeblina;
}