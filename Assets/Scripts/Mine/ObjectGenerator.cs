using UnityEngine;
using System.Collections;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject housePrefab;
    public GameObject treePrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public int numberOfHouses = 2;
    public int numberOfTrees = 5;
    public float fixedY = 0f; // Fixed Y-axis value

    private bool isGenerating = true;

    private void Start()
    {
        SkiSlopeGenerator slopeGenerator = FindObjectOfType<SkiSlopeGenerator>();
        if (slopeGenerator != null)
        {
            StartCoroutine(SpawnAfterGeneration(slopeGenerator));
        }
        else
        {
            Debug.LogError("SkiSlopeGenerator not found in the scene!");
        }
    }

    private IEnumerator SpawnAfterGeneration(SkiSlopeGenerator slopeGenerator)
    {
        while (isGenerating)
        {
            yield return null; // Wait until generation is complete
        }

        GenerateObjects(slopeGenerator);
    }

    private void GenerateObjects(SkiSlopeGenerator slopeGenerator)
    {
        GenerateHouses(slopeGenerator);
        GenerateTrees(slopeGenerator);
        SpawnPlayerAndEnemy(slopeGenerator);
    }

    private void GenerateHouses(SkiSlopeGenerator slopeGenerator)
    {
        for (int i = 0; i < numberOfHouses; i++)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint(slopeGenerator);
            spawnPoint.y = fixedY; // Set fixed Y-axis value
            Instantiate(housePrefab, spawnPoint, Quaternion.identity);
        }
    }

    private void GenerateTrees(SkiSlopeGenerator slopeGenerator)
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint(slopeGenerator);
            spawnPoint.y = fixedY; // Set fixed Y-axis value
            Instantiate(treePrefab, spawnPoint, Quaternion.identity);
        }
    }

    private void SpawnPlayerAndEnemy(SkiSlopeGenerator slopeGenerator)
    {
        // Calculate spawn points on opposite sides of the mesh
        float spawnHeight = 2f; // Adjust this value as needed
        Vector3 playerSpawnPoint = slopeGenerator.transform.position + new Vector3(0, spawnHeight, slopeGenerator.ySize / 2);
        Vector3 enemySpawnPoint = slopeGenerator.transform.position + new Vector3(0, spawnHeight, -slopeGenerator.ySize / 2);

        // Instantiate player and enemy objects at calculated spawn points
        Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity);
        Instantiate(enemyPrefab, enemySpawnPoint, Quaternion.identity);
    }


    private Vector3 GetRandomSpawnPoint(SkiSlopeGenerator slopeGenerator)
    {
        Mesh mesh = slopeGenerator.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int randomVertexIndex = Random.Range(0, vertices.Length);
        return slopeGenerator.transform.TransformPoint(vertices[randomVertexIndex]);
    }

    public void GenerationComplete()
    {
        isGenerating = false;
    }
}
