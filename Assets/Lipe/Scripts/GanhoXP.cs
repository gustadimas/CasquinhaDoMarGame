using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GanhoXP : MonoBehaviour
{
    [SerializeField] Slider barraXP;
    public static int xp;

    public event Action<bool> OnLevelUp;

    // Update is called once per frame
    void Update()
    {
        barraXP.value = xp;

        if (barraXP.value >= 5)
        {
            OnLevelUp(true);
            xp = 0;
        }
    }
}
