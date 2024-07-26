using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuvemLixo : MonoBehaviour
{
    bool dentroDaNuvem = false;
    IEnumerator DanoContinuo()
    {
        while (dentroDaNuvem)
        {
            yield return new WaitForSeconds(1);
            StatusTartaruga status = FindObjectOfType<StatusTartaruga>();
            status.ReceberDano(0.1f);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tartaruga"))
        {
            StartCoroutine(DanoContinuo());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tartaruga"))
        {
            dentroDaNuvem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tartaruga"))
        {
            dentroDaNuvem = false;
        }
    }
}
