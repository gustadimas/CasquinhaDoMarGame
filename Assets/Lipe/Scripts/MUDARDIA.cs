using TMPro;
using UnityEngine;

public class MUDARDIA : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mudoudia;
    GanhoXP ganharXP;
    void Start()
    {
        ganharXP = FindObjectOfType<GanhoXP>();
        ganharXP.OnLevelUp += GanharXP_OnLevelUp;
        mudoudia.enabled = false;
    }

    private void GanharXP_OnLevelUp(bool upou)
    {
        if (upou)
        {
            mudoudia.enabled = true;
            Invoke(nameof(Sumir), 3f);
        }
    }

    void Sumir()
    {
        mudoudia.enabled = false;
    }
}
