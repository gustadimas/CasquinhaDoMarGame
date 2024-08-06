using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;

public class EventController : MonoBehaviour
{
    public event Action<string> OnRandomizedEvent;

    public static bool eventoEmAndamento;

    private List<string> eventos = new List<string> { "EventoLixo", "EventoPescador", "EventoTurista" };

    void Start()
    {
        eventoEmAndamento = false;

        EmbaralharListinha();

        StartCoroutine(SortearEvento());
    }

    IEnumerator SortearEvento()
    {
        while (true)
        {
            yield return new WaitUntil(() => !eventoEmAndamento);
            yield return new WaitForSeconds(7f);

            if (OnRandomizedEvent != null)
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
