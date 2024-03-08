using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform cam;
    private void Awake()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos =  x;
                float yPos =y;
                var spawnTile = Instantiate(tilePrefab, new Vector3(xPos, yPos), Quaternion.identity);
                spawnTile.name = $"Tile {x}{y}";

                var isOffset = (x % 2 == 0 && y % 1 != 0) || (x % 2 != 0 && y % 2 == 0);
                //  spawnTile.tileColor(isOffset);

            }
        }
        cam.transform.position = new Vector3((float)width /4+1.5f, (float)height / 4, -10);
 
    }
}