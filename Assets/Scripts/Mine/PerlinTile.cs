using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PerlinTile : MonoBehaviour
{
    public int gridSize = 10; // Size of the grid
    public float noiseScale = 1f; // Scale of the noise
    public float heightScale = 1f; // Scale of the height
    public float tileSize = 1f; // Size of each tile

    private Mesh mesh;
    private Vector3[] vertices;

    private void Awake()
    {
        GenerateTile();
    }

    private void GenerateTile()
    {
        // Create a new mesh
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Perlin Tile";

        // Calculate the number of vertices in the grid
        int numVertices = (gridSize + 1) * (gridSize + 1);

        // Generate vertices
        vertices = new Vector3[numVertices];
        for (int z = 0, i = 0; z <= gridSize; z++)
        {
            for (int x = 0; x <= gridSize; x++, i++)
            {
                // Calculate the position of the vertex
                float xPos = x * tileSize;
                float zPos = z * tileSize;
                float yPos = Mathf.PerlinNoise(xPos * noiseScale, zPos * noiseScale) * heightScale;

                vertices[i] = new Vector3(xPos, yPos, zPos);
            }
        }

        // Assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();
    }

    private int[] GenerateTriangles()
    {
        int numTiles = gridSize * gridSize;
        int numTriangles = numTiles * 2;
        int numVertices = gridSize + 1;
        int[] triangles = new int[numTriangles * 3];

        for (int ti = 0, vi = 0, y = 0; y < gridSize; y++, vi++)
        {
            for (int x = 0; x < gridSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + numVertices;
                triangles[ti + 5] = vi + numVertices + 1;
            }
        }

        return triangles;
    }
}
