using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaparecerNPCs : MonoBehaviour
{
    [SerializeField] Renderer[] meshRenderer;
    IEnumerator Desaparecer()
    {
        Color corzinha = meshRenderer[0].material.color;
        while (corzinha.a > 0)
        {
            yield return null;
            corzinha.a -= 1 * Time.deltaTime;
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = corzinha;
            }
        }
        Destroy(transform.parent.gameObject);
    }

    public void Call_Desaparecer()
    {
        StartCoroutine(Desaparecer());
    }
}
