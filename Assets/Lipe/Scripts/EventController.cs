using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public event Action<string> OnRandomizedEvent;

    public static bool eventoEmAndamento;

    private List<string> eventos = new List<string> { "EventoLixo", "EventoPescador", "EventoTurista" };

    // Start is called before the first frame update
    void Start()
    {
        eventoEmAndamento = false;

        EmbaralharListinha();

        InvokeRepeating(nameof(SortearEvento), 0f, 5f);
    }

    void SortearEvento()
    {
        if (OnRandomizedEvent != null && !eventoEmAndamento)
        {
            if (eventos.Count == 0)
            {
                eventos = new List<string> { "EventoLixo", "EventoPescador", "EventoTurista" };
                EmbaralharListinha();
            }

            string eventoSorteado = eventos[0];
            eventos.RemoveAt(0);
            OnRandomizedEvent(eventoSorteado);
        }
    }

    void EmbaralharListinha()
    {
        for (int i = 0; i < eventos.Count; i++)
        {
            string eventoTemporario = eventos[i];
            int aleatorio = UnityEngine.Random.Range(i, eventos.Count);
            eventos[i] = eventos[aleatorio];
            eventos[aleatorio] = eventoTemporario;
        }
    }
}
