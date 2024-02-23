using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MyTailGrid : MonoBehaviour
{
    public int xSize, ySize;

    private Mesh mesh;
    private Vector3[] vertices;

    public float tailRad = 1.0f;

    private void Awake()
    {
        StartCoroutine(Generate());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Generate()
    {

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Tail Grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        //Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        Vector3[] normals = new Vector3[vertices.Length];
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        float myangle, myangleRad;
        float ux, uy;
        Quaternion myq;
        Vector3 normls;

        for (int i = 0, y = 0; y <= ySize; y++)
        {

            for (int x = 0; x <= xSize; x++, i++)
            {
                float taperRad = tailRad * (xSize - (float)x) / xSize;
                float circumference = Mathf.PI * 2.0f * taperRad;
                float uvScaleA = Mathf.Max(1, Mathf.Round(circumference)) / circumference;
                float uvScaleL = xSize;
                myangle = 360.0f*(float)y/ ySize;
                myangleRad = myangle * Mathf.Deg2Rad;
                myq = Quaternion.AngleAxis(myangle, Vector3.right);
                vertices[i] = taperRad * (myq * Vector3.up) + 4.0f* Vector3.right * (float)x;
                uy = (myangleRad * taperRad) * uvScaleA;
                ux = vertices[i].x * uvScaleL;
                uv[i] = new Vector2(ux,uy);
                normls = (vertices[i] - Vector3.right * (float)x);
                normals[i] = normls.normalized;
                //normals[i] = (vertices[i]-Vector3.right * (float)x);
                //normals[i] = normals[i].normalized;
                yield return wait;
            }
        }
        mesh.vertices = vertices;
        Debug.Log("mverts" + mesh.vertices.Length);
        Debug.Log("verts" + vertices.Length);
        mesh.normals = normals;
        mesh.uv = uv;
        //mesh.RecalculateTangents();
        //mesh.tangents = tangents;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                mesh.triangles = triangles;
                yield return wait;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateTangents();
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
