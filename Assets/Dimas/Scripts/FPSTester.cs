using UnityEngine;

public class FPSTester : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update() => deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

    void OnGUI()
    {
        int _w = Screen.width, _h = Screen.height;

        GUIStyle _style = new GUIStyle();

        float _offsetX = 40f;
        float _offsetY = 20f;

        Rect _rect = new Rect(_offsetX, _h * 2 / 100 + _offsetY, _w, _h * 2 / 100);
        _style.alignment = TextAnchor.UpperLeft;
        _style.fontSize = _h * 2 / 100;
        _style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float _msec = deltaTime * 1000.0f;
        float _fps = 1.0f / deltaTime;
        string _text = string.Format("{0:0.0} ms ({1:0.} fps)", _msec, _fps);
        GUI.Label(_rect, _text, _style);
    }
}