using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Based on https://gamedevacademy.org/complete-guide-to-procedural-level-generation-in-unity-part-1/

public class MyLevelGenInf : MonoBehaviour
{
    [SerializeField]
    private int mapWidthInTiles, mapDepthInTiles;

    [SerializeField]
    private GameObject tilePrefab;

    List<GameObject> tilePrefabs;
    int numTiles;
    void Start()
    {
        numTiles = mapWidthInTiles * mapDepthInTiles;
        tilePrefabs = new List<GameObject>();
        for (int i = 0; i < numTiles; i++)
        {
            GameObject obj = (GameObject)Instantiate(tilePrefab);
            obj.SetActive(false);
            tilePrefabs.Add(obj);
        }
        GenerateMap();
        //this.GetComponent<MeshRenderer>().bounds.size;

    }

    void Update()
    {

        //UpdateMap();
    }

    void UpdateMap()
        //need to have a list of tiles so we can get their positions and compare to the player position
        //then we need to add a row and destroy a row in x and/or z keeping the total size constant
        //it seems like we don't want to carry a whole separate list of all the mesh data, but can the list be pointers to it?
        //then we just check the boundaries

    {   // get the tile dimensions from the tile Prefab
        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;
        //go through the list of tiles and move postion based on player postion
        //when player goes to a new tile, shift tiles (use position or event)
        //keep the numbers associated with postions from incrementing to infinity
        //by resetting or by making player always at center and move terrain 
        //here 'shift' means that we are going to use object pooling where we
        //maintain a finite list of tile objects and cycle the one we don't want
        //to the one we want. That is we are simply re-using the tile. This means 
        //we need to update the position and texture properly
    }
    void GenerateMap()
    {
        // get the tile dimensions from the tile Prefab
        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;

        // for each Tile, instantiate a Tile in the correct position
        for (int xTileIndex = 0; xTileIndex < mapWidthInTiles; xTileIndex++)
        {
            for (int zTileIndex = 0; zTileIndex < mapDepthInTiles; zTileIndex++)
            {
                // calculate the tile position based on the X and Z indices
                Vector3 tilePosition = new Vector3(
                    this.gameObject.transform.position.x + xTileIndex * tileWidth,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z + zTileIndex * tileDepth);
                // instantiate a new Tile
                //update to turn on the tile from the list in the appropriate position
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as GameObject;
            }
        }
    }
}
