using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ControladorAgua : MonoBehaviour
{
    MeshFilter filtroMalha;

    private void Awake()
    {
        filtroMalha = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Vector3[] vertices = filtroMalha.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = ControladorOnda.instance.AlturaOnda(transform.position.x + vertices[i].x);
        }

        filtroMalha.mesh.vertices = vertices;
        filtroMalha.mesh.RecalculateNormals();
    }
}
