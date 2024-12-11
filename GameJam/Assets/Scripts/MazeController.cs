using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class MazeController : MonoBehaviour
{
    public int width = 50;
    public int height = 50;
    public int checkpointsCount = 30;
    public int minCheckpointRange = 10; // min range between checkpoints
    public int maxCheckpointRange = 25; // max range between checkpoints
    public GameObject WallPrefab;
    public GameObject FloorPrefab;
    public GameObject PlayerPrefab;
    private GameObject player;
    public GameObject EntrancePrefab;
    public GameObject ExitPrefab;
    public GameObject[] PowerupPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var mazeArray = MazeGenerator.GenerateMaze(width, height, checkpointsCount, minCheckpointRange, maxCheckpointRange);
        GenerateMap(mazeArray);
    }

    void GenerateMap(Cell[,] Map)
    {
        for (int y = 0; y < Map.GetLength(0); y++)
        {
            for (int x = 0; x < Map.GetLength(1); x++)
            {
                Vector3 positionWall = new Vector3(x * WallPrefab.transform.localScale.x * 2, 0, y * WallPrefab.transform.localScale.z * 2);
                Vector3 positionFloor = new Vector3(x * FloorPrefab.transform.localScale.x * 2, -1, y * FloorPrefab.transform.localScale.z * 2);
                Vector3 positionPowerup = new Vector3(x * FloorPrefab.transform.localScale.x * 2, 1, y * FloorPrefab.transform.localScale.z * 2);

                if (Map[y, x] == Cell.Wall)
                {
                    Instantiate(WallPrefab, positionWall, Quaternion.identity, transform);
                }
                else if (Map[y, x] == Cell.Path)
                {
                    Instantiate(FloorPrefab, positionFloor, Quaternion.identity, transform);
                }
                else if (Map[y, x] == Cell.Checkpoint)
                {
                    Instantiate(FloorPrefab, positionFloor, Quaternion.identity, transform);
                    var rand = new System.Random();
                    var powerup = PowerupPrefab[rand.Next(0, PowerupPrefab.Count())];
                    Instantiate(powerup, positionPowerup, Quaternion.identity, transform);
                }
                else if (Map[y, x] == Cell.Entrance) 
                {
                    Instantiate(PlayerPrefab, positionFloor, Quaternion.identity, transform);
                    GameObject newEntrance = Instantiate(EntrancePrefab, positionFloor, Quaternion.identity, transform);

                    Vector3 rotation = newEntrance.transform.eulerAngles;
                    rotation.x = -90;
                    newEntrance.transform.eulerAngles = rotation;
                }
                else if (Map[y, x] == Cell.Exit) {
                    GameObject newExit = Instantiate(ExitPrefab, positionFloor, Quaternion.identity, transform);

                    Vector3 rotation = newExit.transform.eulerAngles;
                    rotation.x = -90;
                    newExit.transform.eulerAngles = rotation;
                }
            }
        }

        for (int y=0; y<height+1; y++) {
            for (int x=0; x<width+1; x++) {
                if (x == 0 || y == 0 || x == width || y == height) {
                    Vector3 positionWall = new Vector3((x - 1) * WallPrefab.transform.localScale.x * 2, 0, (y - 1) * WallPrefab.transform.localScale.z * 2);
                    Instantiate(WallPrefab, positionWall, Quaternion.identity, transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
