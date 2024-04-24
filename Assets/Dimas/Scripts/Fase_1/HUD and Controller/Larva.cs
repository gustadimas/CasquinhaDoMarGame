using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larva : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private int missaoID;

    public void TakeDamage(float damage) 
    {
        health -= damage;
        if(health != 0) 
        {
            transform.localScale -= new Vector3(1,1,1) * 0.5f * Time.deltaTime;
        }
        if(health <= 0) 
        {
			health = 0;
            DestroyLarva();
		}
    }

	private void DestroyLarva() 
    {
        FindAnyObjectByType<QuestController>().AtualizarProgressoMissoes(missaoID, 1);
        gameObject.SetActive(false);
    }
}

