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
    private Vector3 runnerPos;
    public int xTriggerMax, xTriggerMin, zTriggerMax, zTriggerMin;
    public int edgeTiles;
    Vector3 tileSize;
    Vector3 worldCenter, terrainOrigin;
    private Vector3 terrainCenter;
    float terrainWidth, terrainDepth;
    public GameObject theRunner;
    public RunnerController runnerController;

    public Vector3 TerrainCenter
    {
        get { return terrainCenter; }
        set { }
    }

    void Start()
    {
        //set up the list of tiles to create an object pool of tiles.
        //The pool can be larger than the actual number needed, so 
        //that the terrain can be expanded and contracted a bit.
        //Add extra rows in each direction so we have extra tiles
        //in the pool
        numTiles = (mapWidthInTiles + 2) * (mapDepthInTiles + 2);

        //Generate the tile pool
        tilePrefabs = new List<GameObject>();
        for (int i = 0; i < numTiles; i++)
        {
            GameObject obj = (GameObject)Instantiate(tilePrefab);
            obj.SetActive(false); //turn it on later
            tilePrefabs.Add(obj); //add the tile to the pool
        }

        //set the game world orgin for reference
        //this will allow us to reset the center position with respect
        //to the numerical value of the position so that the 
        //positions do not grow too large
        worldCenter = Vector3.zero;
        terrainOrigin = worldCenter;
        //this.gameObject.transform.position = worldCenter;

        GenerateMap();

        //get terrain dimensions
        tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;
        terrainWidth = tileSize.x * mapWidthInTiles;
        terrainDepth = tileSize.z * mapDepthInTiles;
        terrainCenter = terrainOrigin + (0.5f * ((Vector3.right * terrainWidth) + (Vector3.forward * terrainDepth)));
        //Debug.Log("terrainCenter " + terrainCenter);

        //Get the runner object and set its initial center postion
        runnerController = theRunner.GetComponent<RunnerController>();
        runnerController.Pos0 = terrainCenter;

        //Set up a trigger box defined by the number of tiles (integer).
        //First make sure that the edge size is smaller than the map width and depth
        if (2 * edgeTiles > mapDepthInTiles)
        {
            mapDepthInTiles = 4 * edgeTiles;
        }
        if (2 * edgeTiles > mapWidthInTiles)
        {
            mapWidthInTiles = 4 * edgeTiles;
        }
        //Get the initial location of the trigger boundry
        //in integer number of tiles and assuming the world center is zero
        xTriggerMax = tileWidth * (mapWidthInTiles - edgeTiles);
        zTriggerMax = tileDepth * (mapDepthInTiles - edgeTiles);
        xTriggerMin = tileWidth * edgeTiles;
        zTriggerMin = tileDepth * edgeTiles;
        //Debug.Log("Trigger Box " + xTriggerMax + " " + zTriggerMax + " " + xTriggerMin + " " + zTriggerMin);
    }

    void Update()
    {
        UpdateMap();
    }

    void UpdateMap()
    {
        // get the tile dimensions from the tile Prefab
        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;
        int edgeSizex = tileWidth * edgeTiles; 
        int edgeSizez = tileDepth * edgeTiles;

        // get the current runner position
        runnerController = theRunner.GetComponent<RunnerController>();
        runnerPos = runnerController.Pos;
        int runnerPosX = (int)runnerPos.x;
        int runnerPosZ = (int)runnerPos.z;
        //Debug.Log("runnerPos " + runnerPos);
        //Debug.Log("Trigger Box " + xTriggerMax + " " + zTriggerMax + " " + xTriggerMin + " " + zTriggerMin);

        //turn on a row of tiles after runner hits the event boundary
        //then turn off opposite row
        if (runnerPosX > xTriggerMax)
        {
            Debug.Log("xMax");
            for (int zTileIndex = 0; zTileIndex < mapDepthInTiles; zTileIndex++)
            {
                int xTileIndex = mapWidthInTiles;
                Vector3 tilePosition = new Vector3(
                    this.gameObject.transform.position.x + xTileIndex * tileWidth,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z + zTileIndex * tileDepth);

                bool notDone = true;
                for (int i = 0; i < tilePrefabs.Count; i++)
                {
                    //if the tile is free turn it on and set its postion
                    if (!tilePrefabs[i].activeSelf & notDone)
                    {
                        tilePrefabs[i].SetActive(true);
                        tilePrefabs[i].transform.position = tilePosition;
                        notDone = false;
                    }
                    //turn off the tile if it is in the first row
                    if ((int)tilePrefabs[i].transform.position.x < xTriggerMin - edgeSizex)
                    {
                        tilePrefabs[i].SetActive(false);
                    }
                }
            }
            this.gameObject.transform.position += tileWidth * Vector3.right;
            xTriggerMax += tileWidth;
            xTriggerMin += tileWidth;
        }
        else if (runnerPosX < xTriggerMin)
        {
            Debug.Log("xMin");
            for (int zTileIndex = 0; zTileIndex < mapDepthInTiles; zTileIndex++)
            {
                int xTileIndex = 0;
                Vector3 tilePosition = new Vector3(
                    this.gameObject.transform.position.x + xTileIndex * tileWidth,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z + zTileIndex * tileDepth);

                bool notDone = true;
                for (int i = 0; i < tilePrefabs.Count; i++)
                {
                    //if the tile is free turn it on and set its postion
                    if (!tilePrefabs[i].activeSelf & notDone)
                    {
                        tilePrefabs[i].SetActive(true);
                        tilePrefabs[i].transform.position = tilePosition;
                        notDone = false;
                    }
                    //turn off the tile if it is in the last row
                    if ((int)tilePrefabs[i].transform.position.x > xTriggerMax + edgeSizex)
                    {
                        tilePrefabs[i].SetActive(false);
                    }
                }
            }
            this.gameObject.transform.position -= tileWidth * Vector3.right;
            xTriggerMax -= tileWidth;
            xTriggerMin -= tileWidth;
        }


        if (runnerPosZ > zTriggerMax)
        {
            Debug.Log("zMax");
            for (int xTileIndex = 0; xTileIndex < mapWidthInTiles; xTileIndex++)
            {
                int zTileIndex = mapDepthInTiles;
                Vector3 tilePosition = new Vector3(
                    this.gameObject.transform.position.x + xTileIndex * tileWidth,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z + zTileIndex * tileDepth);

                bool notDone = true;
                for (int i = 0; i < tilePrefabs.Count; i++)
                {
                    //if the tile is free turn it on and set its postion
                    if (!tilePrefabs[i].activeSelf & notDone)
                    {
                        tilePrefabs[i].SetActive(true);
                        tilePrefabs[i].transform.position = tilePosition;
                        notDone = false;
                    }
                    //turn off the tile if it is in the first row
                    if ((int)tilePrefabs[i].transform.position.z < (zTriggerMin - edgeSizez))
                    {
                        tilePrefabs[i].SetActive(false);
                    }
                }
            }
            this.gameObject.transform.position += tileDepth * Vector3.forward;
            zTriggerMax += tileDepth;
            zTriggerMin += tileDepth;
        }
        else if (runnerPosZ < zTriggerMin)
        {
            Debug.Log("zMin");
            for (int xTileIndex = 0; xTileIndex < mapWidthInTiles; xTileIndex++)
            {
                int zTileIndex = 0;
                Vector3 tilePosition = new Vector3(
                    this.gameObject.transform.position.x + xTileIndex * tileWidth,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z + zTileIndex * tileDepth);

                bool notDone = true;
                for (int i = 0; i < tilePrefabs.Count; i++)
                {
                    //if the tile is free turn it on and set its postion
                    if (!tilePrefabs[i].activeSelf & notDone)
                    {
                        tilePrefabs[i].SetActive(true);
                        tilePrefabs[i].transform.position = tilePosition;
                        notDone = false;
                    }
                    //turn off the tile if it is in the last row
                    if ((int)tilePrefabs[i].transform.position.z > (zTriggerMax + edgeSizez))
                    {
                        tilePrefabs[i].SetActive(false);
                    }
                }
            }
            this.gameObject.transform.position -= tileDepth * Vector3.forward;
            zTriggerMax -= tileDepth;
            zTriggerMin -= tileDepth;
        }

            //turn off tiles that are beyond the horizon
            //for (int i = 0; i < tilePrefabs.Count; i++)
            //{
            //    if (tilePrefabs[i].activeSelf)
            //    {
            //        Vector3 tilePos = tilePrefabs[i].transform.position;
            //        Vector3 distance = runnerPos - tilePos;
            //        if (distance.z > horizonz)
            //        {
            //            tilePrefabs[i].SetActive(false);
            //        }
            //        if (distance.x < 0 & Mathf.Abs(distance.x) > horizonx)
            //        {
            //            tilePrefabs[i].SetActive(false);
            //            xmin += tileSize.x;
            //        }
            //    }
            //}

     }
    void GenerateMap()
    {
        // get the tile dimensions from the tile Prefab
        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;

        this.gameObject.transform.position = terrainOrigin;
        // Set the tile positions for each tile in the list and make them active
        for (int i = 0, xTileIndex = 0; xTileIndex < mapWidthInTiles; xTileIndex++)
        {
            for (int zTileIndex = 0; zTileIndex < mapDepthInTiles; zTileIndex++ , i++)
            {
                // calculate the tile position based on the X and Z indices
                Vector3 tilePosition = new Vector3(
                    this.gameObject.transform.position.x + xTileIndex * tileWidth,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z + zTileIndex * tileDepth);
                //update to turn on the tile from the list in the appropriate position
                tilePrefabs[i].transform.position = tilePosition;
                tilePrefabs[i].SetActive(true);
            }
        }
    }
}
