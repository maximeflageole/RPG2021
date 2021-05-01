using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Tilemap m_groundTilemap;
    [SerializeField] private Tilemap m_obstaclesTilemap;
    [SerializeField] private GameObject m_tilePrefab;
    [SerializeField] private Vector3 m_tileOffset;
    private List<Tile> m_availableTiles = new List<Tile>();

    // Start is called before the first frame update
    void Awake()
    {
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
                    m_availableTiles.Add(tile);
                    tile.Init(m_obstaclesTilemap.HasTile(pos));

                }
            }
        }
    }

    private void BuildMap()
    {

    }
}
