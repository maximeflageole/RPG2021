using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Vector2Int m_pos;
    public bool IsObstructed { get; private set; }
    public bool IsOccupied { get; private set; }
    [SerializeField] private int m_travelCost = 1;
    [SerializeField] private SpriteRenderer m_defaultTile;
    [SerializeField] private SpriteRenderer m_obstructedTile;
    [SerializeField] private SpriteRenderer m_traversableTile;

    public void Init(bool obstructed, Vector2Int pos)
    {
        m_pos = pos;
        m_defaultTile.enabled = !obstructed;
        m_obstructedTile.enabled = obstructed;
    }
}
