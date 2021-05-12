using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public static Map Instance;

    [SerializeField] private Tilemap m_groundTilemap;
    [SerializeField] private Tilemap m_obstaclesTilemap;
    [SerializeField] private GameObject m_tilePrefab;
    [SerializeField] private Vector3 m_tileOffset;
    private Dictionary<Vector2Int, Tile> m_availableTiles = new Dictionary<Vector2Int, Tile>();
    private List<Vector2Int> m_neighboursOffsets = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0) };

    // Start is called before the first frame update
    void Awake()
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        var cellBounds = m_groundTilemap.cellBounds;
        for (int i = cellBounds.xMin; i < cellBounds.xMax; i++)
        {
            for (int j = cellBounds.yMin; j < cellBounds.yMax; j++)
            {
                var pos = new Vector3Int(i, j, 0);
                if (m_groundTilemap.HasTile(pos))
                {
                    var instancePos = m_groundTilemap.CellToWorld(pos) + m_tileOffset;
                    var tile = Instantiate(m_tilePrefab, instancePos, Quaternion.identity, transform).GetComponent<Tile>();
                    var vec2 = new Vector2Int(i, j);
                    m_availableTiles.Add(new Vector2Int(i, j), tile);
                    tile.Init(m_obstaclesTilemap.HasTile(pos), vec2);
                    //if (!m_obstaclesTilemap.HasTile(pos))
                    //Debug.Log("pos " + pos);
                }
            }
        }
    }

    public bool TryGetTileAtPos(Vector2Int pos, ref Tile value)
    {
        return m_availableTiles.TryGetValue(pos, out value);
    }

    public void DisplayRange(Tile initialTile, int range, ETileAccessType accessType = ETileAccessType.Free, bool considerCost = true)
    {
        var tilesInReach = AStar.DisplayRange(this, initialTile, range);
        foreach (var tile in tilesInReach)
        {
            tile.DisplayTraversable(true);
        }
    }

    public void HideRange()
    {
        foreach (var tile in m_availableTiles.Values)
        {
            tile.DisplayTraversable(false);
        }
    }

    public List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> list = new List<Tile>();
        foreach (var neighbourTile in m_neighboursOffsets)
        {
            var calculatedNeighbour = neighbourTile + tile.Pos;
            if (m_availableTiles.ContainsKey(calculatedNeighbour))
            {
                list.Add(m_availableTiles[calculatedNeighbour]);
            }
        }
        return list;
    }
}

public enum ETileAccessType
{
    Free,
    Obstructed,
    Owned,
    OwnedByAlly,
    OwnedByEnemy,
    Count
}