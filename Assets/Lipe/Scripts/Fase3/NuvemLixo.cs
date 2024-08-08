using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuvemLixo : MonoBehaviour
{
    bool dentroDaNuvem = false;
    [SerializeField] float danoCausado;
    IEnumerator DanoContinuo()
    {
        while (dentroDaNuvem)
        {
            StatusTartaruga status = FindObjectOfType<StatusTartaruga>();
            status.ReceberDano(danoCausado);
            yield return new WaitForSeconds(1);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tartaruga"))
        {
            dentroDaNuvem = true;
            StartCoroutine(DanoContinuo());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tartaruga"))
            dentroDaNuvem = false;
    }
}