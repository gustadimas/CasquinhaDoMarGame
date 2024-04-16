using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public event Action<string> OnRandomizedEvent;

    public static bool eventoEmAndamento;

    // Start is called before the first frame update
    void Start()
    {
        eventoEmAndamento = false;

        InvokeRepeating(nameof(SortearEvento), 0f, 5f);
    }

    void SortearEvento()
    {
        if (OnRandomizedEvent != null && !eventoEmAndamento)
        {
            int eventoSorteado = UnityEngine.Random.Range(0, 2);
            switch (eventoSorteado)
            {
                case 0:
                    OnRandomizedEvent("EventoLixo");
                    break;

                case 1:
                    OnRandomizedEvent("EventoPescador");
                    break;

                case 2:
                    OnRandomizedEvent("EventoTurista");
                    break;
            }
        }


    }
}
