using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerFilhotes : MonoBehaviour
{
    [SerializeField] float velocidadeMovimento;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb != null && velocidadeMovimento != 0)
        {
            rb.MovePosition(transform.position + velocidadeMovimento * Time.deltaTime * Vector3.forward);
        }
    }
}