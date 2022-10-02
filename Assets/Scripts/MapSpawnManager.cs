using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public enum TileType
{
    Empty,
    Border,
    EnemySpawner,
    PlayerBuilding,
    EnemySpawnerValid
}

public enum MapEvolution
{
    CreateEnemySpawner,
    UpgradeEnemySpawner,
    
}
public class MapSpawnManager : MonoBehaviour
{
    public int mapSize = 10;

    public List<TileWithData> allTileData;
    [SerializeField] private Tilemap map;

    private float squareSize = 1f;

    public List<MapEvolution> evolutions;
    private int evolutionIndex;
    private TileBase[] allTiles;
    private BoundsInt bounds ;
    private Dictionary<TileType, List<string>> tileDataDict;
    private List<Vector3Int> emptyCoords;

    public GameObject spawnerPrefab;
    public TileBase spawnerTile;
    public Grid grid;

    public int avoidSpawnInGrassFor = 5;
    private void Awake()
    {
        evolutionIndex = 0;
        tileDataDict = new Dictionary<TileType, List<string>>();

        foreach (var td in allTileData)
        {
            if (!tileDataDict.ContainsKey(td.type))
            {
                tileDataDict.Add(td.type, new List<string>());
            }

            foreach (var tb in td.tileList)
            {
                if (!tileDataDict[td.type].Contains(tb.name))
                {
                    tileDataDict[td.type].Add(tb.name);
                }
                else
                {
                    Debug.LogWarning("Duplicate type!");
                }
            }
        }

        emptyCoords = new List<Vector3Int>();
        
        bounds= map.cellBounds;
        allTiles = map.GetTilesBlock(bounds);
    
        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = GetTileFromXY(x,y);
                if (tileDataDict[TileType.EnemySpawnerValid].Contains(tile.name))
                {
                    emptyCoords.Add(new Vector3Int(x,y,0));
                }
                if (tile != null) {
//                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                } else {
                //    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }    
    }


    private TileBase GetTileFromXY(int x, int y)
    {
        return allTiles[x + y * bounds.size.x];
    }

    private Vector3 GetPositionFrom(Vector3Int p)
    {
        return grid.GetCellCenterWorld(p + Vector3Int.left * 7+ Vector3Int.down * 7);
    }


    private void OnEnable()
    {
        GameActions.onPhaseChanged += SpawnSpawnerOrUpgrade;
    }
    private void OnDisable()
    {
        GameActions.onPhaseChanged -= SpawnSpawnerOrUpgrade;
    }

    private void SpawnSpawnerOrUpgrade(Phase phase)
    {
        if (phase == Phase.Build && evolutionIndex < evolutions.Count)
        {
            switch (evolutions[evolutionIndex])
            {
                case MapEvolution.CreateEnemySpawner:
                    CreateSpawner();
                    evolutionIndex++;
                    break;
                case MapEvolution.UpgradeEnemySpawner:
                    UpgradeSpawner();
                    evolutionIndex++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else if(evolutionIndex >= evolutions.Count && GameManager.I.state == GameState.Playing)
        {
            GameManager.I.Win();
        }

    }

    private void UpgradeSpawner()
    {
    }

    private void CreateSpawner()
    {
        if(emptyCoords.Count == 0) return;
        int index = Random.Range(0, emptyCoords.Count);
        map.SetTile(map.WorldToCell(GetPositionFrom(emptyCoords[index])), spawnerTile);
        
        Instantiate(spawnerPrefab,GetPositionFrom(emptyCoords[index]), spawnerPrefab.transform.rotation);
        emptyCoords.RemoveAt(index);
    }
}
