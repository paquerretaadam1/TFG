using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{

    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY
    }

    public Grid[,] GridHandler;
    public List<WalkerObject> Walkers;
    public Tilemap tileMap;
    public Tile Floor;

    public Tile Wall;
    public int MapWidth = 30;
    public int MapHeight = 30;

    public int MaximumWalkers = 10;
    public int TileCount = 0;
    public float FillPercentage = 0.4f;
    public float WaitTime = 0.05f;

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        GridHandler = new Grid[MapWidth, MapHeight];

        for (int x = 0; x < GridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < GridHandler.GetLength(1); y++)
            {
                GridHandler[x, y] = Grid.EMPTY;
            }
        }

        Walkers = new();

        //Se crea como vector 3 porque el método set Tile necesita este tipo de vector 
        Vector3Int TileCenter = new(GridHandler.GetLength(0) / 2, GridHandler.GetLength(1) / 2, 0);
        WalkerObject curWalker = new(new(TileCenter.x, TileCenter.y), GetDirection(), 0.5f);
        GridHandler[TileCenter.x, TileCenter.y] = Grid.FLOOR;
        tileMap.SetTile(TileCenter, Floor);
        Walkers.Add(curWalker);

        TileCount++;

        StartCoroutine(CreateFloors());
    }

    Vector2 GetDirection()
    {
        int choice = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);

        return choice switch
        {
            0 => Vector2.down,
            1 => Vector2.left,
            2 => Vector2.up,
            3 => Vector2.right,
            _ => Vector2.zero,
        };
    }

    IEnumerator CreateFloors()
    {
        while ((float)TileCount / (float)GridHandler.Length < FillPercentage)
        {
            bool hasCreatedFloor = false;
            foreach (WalkerObject curWalker in Walkers)
            {
                Vector3Int curPos = new((int)curWalker.Position.x, (int)curWalker.Position.y, 0);

                if (GridHandler[curPos.x, curPos.y] != Grid.FLOOR)
                {
                    tileMap.SetTile(curPos, Floor);
                    TileCount++;
                    GridHandler[curPos.x, curPos.y] = Grid.FLOOR;
                    hasCreatedFloor = true;
                }
            }

            ChanceToRemove();
            ChanceToRedirect();
            ChanceToCreate();
            UpdatePosition();

            if (hasCreatedFloor)
            {
                yield return new WaitForSeconds(WaitTime);
            }
        }
        StartCoroutine(CreateWalls());
    }

    void ChanceToRemove()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && updatedCount > 1)
            {
                Walkers.RemoveAt(i);
                break;
            }
        }
    }

    void ChanceToRedirect()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange)
            {
                WalkerObject curWalker = Walkers[i];
                curWalker.Direction = GetDirection();
                Walkers[i] = curWalker;
            }
        }
    }

    void ChanceToCreate()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange)
            {
                WalkerObject curWalker = Walkers[i];
                curWalker.Direction = GetDirection();
                Walkers[i] = curWalker;
            }
        }
    }

    void UpdatePosition()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            WalkerObject FoundWalker = Walkers[i];

            FoundWalker.Position += FoundWalker.Direction;
            FoundWalker.Position.x = Mathf.Clamp(FoundWalker.Position.x, 1, GridHandler.GetLength(0) - 2);
            FoundWalker.Position.y = Mathf.Clamp(FoundWalker.Position.y, 1, GridHandler.GetLength(1) - 2);
            Walkers[i] = FoundWalker;

        }
    }

    IEnumerator CreateWalls()
    {
        for (int x = 0; x < GridHandler.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < GridHandler.GetLength(1) - 1; y++)
            {
                if (GridHandler[x, y] == Grid.FLOOR)
                {
                    bool hasCreatedWall = false;
                    int auxX = 0;
                    int auxY = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        // Si el identador es cero o uno se comprueban la posiciones x + 1 y x - 1
                        auxX = i switch
                        {
                            0 => 1 + x,
                            1 => -1 + x,
                            _ => x
                        };
                        // Si el identador es dos o tres se comprueban la posiciones y + 1 e y - 1
                        auxY = i switch
                        {
                            2 => 1 + y,
                            3 => -1 + y,
                            _ => y
                        };
                        if (GridHandler[auxX, auxY] == Grid.EMPTY)
                        {
                            tileMap.SetTile(new(auxX, auxY, 0), Wall);
                            GridHandler[auxX, auxY] = Grid.WALL;
                            hasCreatedWall = true;
                        }
                    }

                    if (hasCreatedWall)
                    {
                        yield return new WaitForSeconds(WaitTime);
                    }
                }
            }
        }
    }
}
