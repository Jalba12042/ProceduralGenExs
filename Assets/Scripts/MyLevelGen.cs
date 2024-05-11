using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLevelGen : MonoBehaviour
{
    [SerializeField] private int mapWidthInTiles, mapDepthInTiles;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject housePrefab; // Prefab for the house
    [SerializeField] private float noiseAmount = 0.5f; // Noise amount variable

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // Get the tile dimensions from the tile Prefab
        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;

        // Loop through each tile
        for (int xTileIndex = 0; xTileIndex < mapWidthInTiles; xTileIndex++)
        {
            for (int zTileIndex = 0; zTileIndex < mapDepthInTiles; zTileIndex++)
            {
                // Calculate the tile position based on the X and Z indices
                Vector3 tilePosition = new Vector3(
                    transform.position.x + xTileIndex * tileWidth,
                    transform.position.y,
                    transform.position.z + zTileIndex * tileDepth);

                // Instantiate a new Tile
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);

                // Spawn trees and houses within the tile
                SpawnObjects(tile);
            }
        }
    }

    void SpawnObjects(GameObject tile)
    {
        // Determine how many trees you want to spawn on each tile
        int numTrees = Random.Range(1, 4); // Example: spawn between 1 to 3 trees

        // Loop to spawn each tree
        for (int i = 0; i < numTrees; i++)
        {
            // Calculate a random position within the tile
            Vector3 treePosition = new Vector3(
                tile.transform.position.x + Random.Range(-5f, 5f), // Adjust the range as needed
                tile.transform.position.y,
                tile.transform.position.z + Random.Range(-5f, 5f)); // Adjust the range as needed

            // Instantiate a tree at the calculated position
            GameObject tree = Instantiate(treePrefab, treePosition, Quaternion.identity);
            tree.transform.SetParent(tile.transform); // Set the tile as the parent of the tree
        }

        // Randomly spawn two houses
        /*for (int i = 0; i < 2; i++)
        {
            // Calculate a random position within the tile
            Vector3 housePosition = new Vector3(
                tile.transform.position.x + Random.Range(-5f, 5f), // Adjust the range as needed
                tile.transform.position.y,
                tile.transform.position.z + Random.Range(-5f, 5f)); // Adjust the range as needed

            // Instantiate a house at the calculated position
            GameObject house = Instantiate(housePrefab, housePosition, Quaternion.identity);
            house.transform.SetParent(tile.transform); // Set the tile as the parent of the house
        }*/
    }

    // Public method to get the noise amount
    public float GetNoiseAmount()
    {
        return noiseAmount;
    }
}
