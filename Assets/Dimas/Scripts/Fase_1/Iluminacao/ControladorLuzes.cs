using UnityEngine;

[ExecuteAlways]
public class ControladorLuzes : MonoBehaviour
{
    [SerializeField] Light luzDirecional;

    private void OnValidate()
    {
        if (luzDirecional != null) return;

        if (RenderSettings.sun != null) luzDirecional = RenderSettings.sun;
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